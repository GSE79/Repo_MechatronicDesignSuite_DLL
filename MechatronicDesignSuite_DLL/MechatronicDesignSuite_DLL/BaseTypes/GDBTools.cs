using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using MechatronicDesignSuite_DLL.BaseNodes;

namespace MechatronicDesignSuite_DLL.BaseTypes
{
    #region // *******************************************GDB Parser and Helper Classes

    ////////////////////////////////////////////////
    // Class - GDB BackGround Worker CMD Queue Item
    public class GDBCommandQueueItem
    {
        public bool ExtractArchive = false;                     // trigger to extract the *.zip archive
        public bool RunReadElf = false;                         // trigger to run readelf.exe
        public bool RunGDB = false;                             // trigger to run GDB with Batch CMDs
        public bool BuildSymbolTree = false;                    // trigger to parse GDB and build symbol tree
        public Exception GDBCMDException = null;                // thread execution exception return value
        public List<string> GDBBatchLines = new List<string>(); // List of batch line for use in GDB call
        public string ReadElfArgs = "";                         // command arguments to readelf.exe
        public string GDBArgs = "";                             // command arguments to gdb.exe
        public TreeNode elfNode, SMNode;
    }

    
    ///////////////////////////////////////////////
    // Class - GDBParser
    public class GDBParser
    {

        const char spaceChar = ' ';
        public string STDOutString = "";
        public string STDErrString = "";

        public TreeNode CreateSMTreeNode(ref string StdOutString)
        {

            // Cretae stack variables
            TreeNode SMNode = new TreeNode();
            TreeNode CurrentParentNode, CurrentChildNode = new TreeNode();
            treeNodeSymbolTag newTypeSymTag;
            treeNodeSymbolTag CurrentListSymbolTag = new treeNodeSymbolTag();
            treeNodeSymbolTag SMNodeTag = new treeNodeSymbolTag(), CurrentParentNodeTag = new treeNodeSymbolTag(), CurrentChildNodeTag = new treeNodeSymbolTag();
            List<treeNodeSymbolTag> typeNodeTags = new List<treeNodeSymbolTag>();
            List<char> tokenChars = new List<char>();
            List<string> lineTokens = new List<string>();
            string SMInfoLine = "";
            int measuredNodeDepth = 0, i;

            // Initialize Parent Node
            CurrentParentNode = SMNode;

            // Break Full Text into Lines
            List<char> tempAList = new List<char>();
            List<char[]> LineList = new List<char[]>();
            foreach (char ASCIICHAR in StdOutString)
            {
                if (ASCIICHAR != '\n') { tempAList.Add(ASCIICHAR); }
                else { LineList.Add(tempAList.GetRange(0, tempAList.Count - 1).ToArray()); tempAList.Clear(); }
            }
            if (LineList.Count > 1)
            {
                SMInfoLine = new string(LineList[0]);
                LineList.RemoveAt(0);
            }

            // Process Lines
            int lineCounter = 0;
            foreach (char[] ASCIILine in LineList)
            {
                if (ASCIILine.Length == 0)
                    break;

                // Measure Node Depth
                measuredNodeDepth = 0;
                if (ASCIILine[0] == spaceChar) { foreach (char tempChar in ASCIILine) { if (tempChar == spaceChar) measuredNodeDepth++; else break; } }

                // Parse tokens to dwarf strings
                #region // Parse line into Tokens
                i = 0;
                lineTokens.Clear();
                tokenChars.Clear();
                foreach (char lineChar in ASCIILine)
                {
                    if (lineChar == spaceChar)
                    {
                        if (ASCIILine.Length > i + 1)
                        {
                            if (ASCIILine[i + 1] != spaceChar)
                            {
                                if (tokenChars.Count > 0)
                                    lineTokens.Add(new string(tokenChars.ToArray()));
                                tokenChars.Clear();
                            }
                        }

                    }
                    else
                    {
                        tokenChars.Add(lineChar);
                    }
                    i++;
                }
                lineTokens.Add(new string(tokenChars.ToArray()));
                #endregion

                #region // Use 1st Token to determine dwarf type information
                if (lineTokens[0] == "type" && lineTokens[1] == "node")          // type node
                {
                    // Always Add new Node
                    typeNodeTags.Add(new treeNodeSymbolTag());
                    CurrentListSymbolTag = typeNodeTags[typeNodeTags.Count - 1];

                    // Always set type node string
                    CurrentListSymbolTag.Dwarfstrings.typeNode = lineTokens[2];



                    // Always latch "node depth"
                    CurrentListSymbolTag.nodeDepth = measuredNodeDepth;

                    // Node line number
                    CurrentListSymbolTag.nodeLineNumber = lineCounter;

                    // determine if a header row preceeded the type node flag and set header string
                    if (lineCounter > 0)
                        CurrentListSymbolTag.Dwarfstrings.childNodeHeader = new string(LineList[lineCounter - 1]);
                    else
                        CurrentListSymbolTag.Dwarfstrings.childNodeHeader = "";

                    if (CurrentListSymbolTag.Dwarfstrings.childNodeHeader.Contains("["))
                        CurrentListSymbolTag.Dwarfstrings.parsedName = CurrentListSymbolTag.Dwarfstrings.childNodeHeader.Substring(CurrentListSymbolTag.Dwarfstrings.childNodeHeader.ToList().FindIndex(x => x == '\'') + 1, (CurrentListSymbolTag.Dwarfstrings.childNodeHeader.ToList().FindLastIndex(x => x == '\'') - CurrentListSymbolTag.Dwarfstrings.childNodeHeader.ToList().FindIndex(x => x == '\'')) - 1);
                    else
                        CurrentListSymbolTag.Dwarfstrings.parsedName = "";

                }
                else if (lineTokens[0] == "name")
                {
                    CurrentListSymbolTag.Dwarfstrings.name = lineTokens[1].Substring(1, lineTokens[1].Length - 1);
                    for (i = 2; i <= lineTokens.FindIndex(x => x.EndsWith("'")); i++)
                        CurrentListSymbolTag.Dwarfstrings.name += " " + lineTokens[i];
                    CurrentListSymbolTag.Dwarfstrings.name = CurrentListSymbolTag.Dwarfstrings.name.Substring(0, CurrentListSymbolTag.Dwarfstrings.name.Length - 1);
                }
                else if (lineTokens[0] == "tagname")
                {
                    CurrentListSymbolTag.Dwarfstrings.tagname = lineTokens[1].Substring(1, lineTokens[1].Length - 1);
                    if (lineTokens.Count > 2)
                        for (i = 2; i <= lineTokens.FindIndex(x => x.EndsWith("'")); i++)
                            CurrentListSymbolTag.Dwarfstrings.tagname += " " + lineTokens[i];
                    CurrentListSymbolTag.Dwarfstrings.tagname = CurrentListSymbolTag.Dwarfstrings.tagname.Substring(0, CurrentListSymbolTag.Dwarfstrings.tagname.Length - 1);
                }
                else if (lineTokens[0] == "code")
                {
                    CurrentListSymbolTag.Dwarfstrings.code = lineTokens[1];
                    if (lineTokens.Count > 2)
                        CurrentListSymbolTag.Dwarfstrings.code += " " + lineTokens[2];
                }
                else if (lineTokens[0] == "length")
                {
                    CurrentListSymbolTag.Dwarfstrings.length = lineTokens[1];
                }
                else if (lineTokens[0] == "target_type")
                {
                    CurrentListSymbolTag.Dwarfstrings.targetType = lineTokens[1];
                    if (CurrentListSymbolTag.Dwarfstrings.targetType != "0x0")
                        CurrentListSymbolTag.isBaseType = true;
                }
                else if (lineTokens[0] == "type_chain")
                {
                    CurrentListSymbolTag.Dwarfstrings.typeChain = lineTokens[1];
                }
                else if (lineTokens[0] == "instance_flags")
                {
                    CurrentListSymbolTag.Dwarfstrings.instanceFlags = lineTokens[1];
                    if (lineTokens.Count > 2)
                        CurrentListSymbolTag.Dwarfstrings.instanceFlags += " " + lineTokens[2];
                }
                else if (lineTokens[0] == "nfields")
                {
                    CurrentListSymbolTag.Dwarfstrings.nfields = lineTokens[1];
                }
                if (lineTokens[0] == "pointer_type")
                {
                    if (CurrentListSymbolTag.Dwarfstrings.pointerType == "")
                        CurrentListSymbolTag.Dwarfstrings.pointerType = lineTokens[1];
                    else
                    {
                        int idex = typeNodeTags.FindIndex(x => x == CurrentListSymbolTag) - 1;
                        if (idex > 0)
                        {
                            if (typeNodeTags[idex].isBaseType)
                            {
                                CurrentListSymbolTag = typeNodeTags[typeNodeTags.Count - 2];
                                CurrentListSymbolTag.Dwarfstrings.pointerType = lineTokens[1];
                            }
                        }
                    }
                }
                #endregion

                // Go to next line
                lineCounter++;
            }

            // Process Type Nodes / Dwarf Strings
            int nodeCounter = 0;
            for (nodeCounter = 0; nodeCounter < typeNodeTags.Count; nodeCounter++)
            {
                newTypeSymTag = typeNodeTags[nodeCounter];

                #region // Initialize SM Node as First (empty) Parent
                if (nodeCounter == 0)
                {
                    CurrentParentNode = SMNode;
                    setSymbolTag(ref CurrentParentNode, ref CurrentParentNodeTag, ref newTypeSymTag);
                    CurrentChildNode = null;
                    CurrentChildNodeTag = null;
                }
                #endregion
                #region // Process all other typeNodes from the list, placing them in nested tree nodes
                else
                {
                    // if the new node is deeper than the current parent
                    if (CurrentParentNodeTag.nodeDepth < newTypeSymTag.nodeDepth)
                    {
                        // if the current child is not null
                        if (CurrentChildNode != null && CurrentChildNodeTag != null)
                        {
                            // if the new node is deepter than the current child
                            if (CurrentChildNodeTag.nodeDepth < newTypeSymTag.nodeDepth)
                            {
                                // Set Parent to Child - Increase Indent - Increase Level
                                CurrentParentNode = CurrentChildNode;
                                CurrentParentNodeTag = (treeNodeSymbolTag)CurrentParentNode.Tag;
                                // Null Child Node references
                                CurrentChildNode = null;
                                CurrentChildNodeTag = null;
                            }

                        }
                    }
                    // if the new node is not deeper than the current parent
                    // and the new node is not the first child of the first parent
                    else if (nodeCounter > 1)
                    {
                        // if the new node is less deep than the current parent
                        if (CurrentParentNodeTag.nodeDepth >= newTypeSymTag.nodeDepth)
                        {
                            bool keepGoing = true;
                            do
                            {
                                // Set Parent to Grand Parent
                                CurrentParentNode = CurrentParentNode.Parent;
                                CurrentParentNodeTag = (treeNodeSymbolTag)CurrentParentNode.Tag;

                                // Stop when parent nodes' depth is "1" less than Current Parent
                                keepGoing = (CurrentParentNodeTag.nodeDepth > (newTypeSymTag.nodeDepth - 4));

                            } while (keepGoing);
                        }

                    }

                    // Add the child node to the parent node
                    CurrentParentNode.Nodes.Add(new TreeNode("new child node"));
                    CurrentChildNode = CurrentParentNode.Nodes[CurrentParentNode.Nodes.Count - 1];
                    // Tag the new child node
                    setSymbolTag(ref CurrentChildNode, ref CurrentChildNodeTag, ref newTypeSymTag);

                }
                #endregion


            }

            //// Process SM node for address mapping
            TreeNode tempNode;
            tempNode = SMNode;
            int startIndexofSubString = SMInfoLine.ToList().FindLastIndex(x => x == 'x') + 1;
            int stopIndexofSubString = SMInfoLine.ToList().FindLastIndex(x => x == '.');
            int lengthofSubString = stopIndexofSubString - startIndexofSubString;

            if (nodeCounter > 1)
            {
                ((treeNodeSymbolTag)(tempNode.Tag)).addrstr = SMInfoLine.Substring(startIndexofSubString, lengthofSubString);
                ((treeNodeSymbolTag)(tempNode.Tag)).addr = ASCIIHex2_Value(((treeNodeSymbolTag)(tempNode.Tag)).addrstr);
                startIndexofSubString = SMInfoLine.ToList().FindIndex(x => x == '\"') + 1;
                stopIndexofSubString = SMInfoLine.ToList().FindLastIndex(x => x == '\"');
                lengthofSubString = stopIndexofSubString - startIndexofSubString;
                ((treeNodeSymbolTag)(tempNode.Tag)).Dwarfstrings.parsedName = SMInfoLine.Substring(startIndexofSubString, lengthofSubString);
                SMNode.ToolTipText += "\nAddr: 0x" + ((treeNodeSymbolTag)(tempNode.Tag)).addr.ToString("X8") + " - ";
                SMNode.Text = ((treeNodeSymbolTag)(tempNode.Tag)).Dwarfstrings.parsedName;
                ((treeNodeSymbolTag)(tempNode.Tag)).longName = "SM";
                SMNode.ToolTipText += "\nLong Name: " + ((treeNodeSymbolTag)(tempNode.Tag)).longName;
                List<TreeNode> thisLevelNodes = new List<TreeNode>();
                List<TreeNode> nextLevelNodes = new List<TreeNode>();

                thisLevelNodes.Add(tempNode);
                foreach (TreeNode tnd in tempNode.Nodes)
                    nextLevelNodes.Add(tnd);

                do
                {
                    // Clock Next Nodes for Processing into Current Nodes for Processing, then clear next nodes
                    thisLevelNodes = new List<TreeNode>(nextLevelNodes);
                    nextLevelNodes.Clear();

                    // Process every node on this level and build list of next level nodes
                    foreach (TreeNode lNode in thisLevelNodes)
                    {
                        ((treeNodeSymbolTag)(lNode.Tag)).longName = ((treeNodeSymbolTag)(lNode.Parent.Tag)).longName;
                        if (((treeNodeSymbolTag)(lNode.Tag)).Dwarfstrings.parsedName != null && ((treeNodeSymbolTag)(lNode.Tag)).Dwarfstrings.parsedName != "")
                            ((treeNodeSymbolTag)(lNode.Tag)).longName = ((treeNodeSymbolTag)(lNode.Tag)).longName + "." + ((treeNodeSymbolTag)(lNode.Tag)).Dwarfstrings.parsedName;
                        ((treeNodeSymbolTag)(lNode.Tag)).addr = ((treeNodeSymbolTag)(lNode.Parent.Tag)).addr;
                        for (i = 0; i < lNode.Index; i++)
                        {
                            if (((treeNodeSymbolTag)((lNode.Parent.Nodes[i]).Tag)).Dwarfstrings.length != null && ((treeNodeSymbolTag)((lNode.Parent.Nodes[i]).Tag)).Dwarfstrings.length != "")
                                ((treeNodeSymbolTag)(lNode.Tag)).addr += uint.Parse(((treeNodeSymbolTag)((lNode.Parent.Nodes[i]).Tag)).Dwarfstrings.length);
                        }
                        ((treeNodeSymbolTag)(lNode.Tag)).addrstr = ((treeNodeSymbolTag)(lNode.Tag)).addr.ToString("X8");
                        lNode.ToolTipText += "\nAddr: 0x" + ((treeNodeSymbolTag)(lNode.Tag)).addr.ToString("X8") + " - ";




                        lNode.ToolTipText += "\nLong Name: " + ((treeNodeSymbolTag)(lNode.Tag)).longName;

                        if (lNode.GetNodeCount(false) > 0)
                            foreach (TreeNode tnd in lNode.Nodes)
                                nextLevelNodes.Add(tnd);
                    }

                } while (nextLevelNodes.Count > 0);
            }


            return SMNode;

        }
        private void setSymbolTag(ref TreeNode Node2Tag, ref treeNodeSymbolTag NodeTag, ref treeNodeSymbolTag typeSymbolTag)
        {
            Node2Tag.Tag = typeSymbolTag;
            NodeTag = typeSymbolTag;
            Node2Tag.ToolTipText = "Line: " + ((treeNodeSymbolTag)Node2Tag.Tag).nodeLineNumber.ToString() + "\n";
            Node2Tag.ToolTipText += ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.childNodeHeader + "\n";
            Node2Tag.ToolTipText += "Size: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes\n";
            Node2Tag.ToolTipText += "Type Node: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.typeNode + " \n";
            Node2Tag.ToolTipText += "Code: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code + "\n";
            Node2Tag.ToolTipText += "Name: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name + "\n";
            Node2Tag.ToolTipText += "TagName: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname + "\n";
            Node2Tag.ToolTipText += "ParsedName: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName + "\n";
            Node2Tag.ToolTipText += "Target Type: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.targetType + "\n";
            Node2Tag.ToolTipText += "Type Chain: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.typeChain + "\n";
            Node2Tag.ToolTipText += "Instance Flags: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.instanceFlags + "\n";
            Node2Tag.ToolTipText += "NFields: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.nfields + "\n";
            Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name + " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname;

            //Node2Tag.ContextMenuStrip = new ContextMenuStrip();
            //Node2Tag.ContextMenuStrip.Items.Add("goto Line");
            //Node2Tag.ContextMenuStrip.Items[Node2Tag.ContextMenuStrip.Items.Count - 1].Tag = Node2Tag;
            //Node2Tag.ContextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(SMMenuItemClicked);
            //Node2Tag.Expand();

            if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name == "'<NULL>'")
            {
                if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.childNodeHeader.Contains('['))
                {
                    int startIndex = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.childNodeHeader.ToCharArray().ToList().FindIndex(x => x == '\'');
                    int stopIndex = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.childNodeHeader.ToCharArray().ToList().FindLastIndex(x => x == '\'');
                    int subStrLength = stopIndex - startIndex;
                    if (subStrLength > 0)
                    {
                        string tempSubStr = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.childNodeHeader.Substring(startIndex + 1, subStrLength - 1);
                        ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name = tempSubStr;
                    }
                    else
                    {
                        ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name = "Un-Named";
                    }


                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname + " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name;
                }

            }
            if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname == "'<NULL>'")
            {
                ;
            }

            if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length != null)
            {
                if (Node2Tag.Text == " " && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length.Length >= 0)
                    Node2Tag.Text = "Type Node: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.typeNode + " - " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";

                if (Node2Tag.Text == "<NULL> <NULL>" && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length.Length > 0)
                    Node2Tag.Text = "Un-Named: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";
                else if (Node2Tag.Text == "<NULL> <NULL>")
                    Node2Tag.Text = "Un-Named: ?? bytes";

                if (!Node2Tag.Text.EndsWith("bytes"))
                    Node2Tag.Text += " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";
            }
            else
            {
                if (Node2Tag.Text == " ")
                    Node2Tag.Text = "Type Node: " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.typeNode + " - ??? bytes";
            }

            Node2Tag.Text += " - - - " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code;

            if (((treeNodeSymbolTag)Node2Tag.Tag).nodeLineNumber == 0)
                ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName = "SM";

            if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != "")
                Node2Tag.Text = string.Concat(((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName, "           ", Node2Tag.Text);

            // Set node name to type string
            Node2Tag.Name = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.typeNode;

            // Set final display text based on node code
            // 0x3 (TYPE_CODE_STRUCT)
            if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x3 (TYPE_CODE_STRUCT)")
            {
                if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != null && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != "" && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != "<NULL>")
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName;
                else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != "<NULL>")
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname;

                Node2Tag.ImageIndex = 20;
                Node2Tag.SelectedImageIndex = Node2Tag.ImageIndex;

            }

            // 0x8 (TYPE_CODE_INT)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x8 (TYPE_CODE_INT)")
            {
                Node2Tag.Parent.Collapse();
                if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != null && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != "")
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName + " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";
                else
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name + " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";

                Node2Tag.ImageIndex = 17;
                Node2Tag.SelectedImageIndex = Node2Tag.ImageIndex;

                if (((treeNodeSymbolTag)Node2Tag.Parent.Tag) != null)
                {
                    if (((treeNodeSymbolTag)(Node2Tag.Parent.Tag)).Dwarfstrings.code == "0x16 (TYPE_CODE_TYPEDEF)")
                        Node2Tag.Parent.ImageIndex = 17;

                    Node2Tag.Parent.SelectedImageIndex = Node2Tag.Parent.ImageIndex;
                }
            }

            // 0x14 (TYPE_CODE_BOOL)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x14 (TYPE_CODE_BOOL)")
            {
                Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName + " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";
            }

            // 0x5 (TYPE_CODE_ENUM)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x5 (TYPE_CODE_ENUM)")
                Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName + " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";

            // 0xc (TYPE_CODE_RANGE)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0xc (TYPE_CODE_RANGE)")
                Node2Tag.Text = "Range";

            // 0x16 (TYPE_CODE_TYPEDEF)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x16 (TYPE_CODE_TYPEDEF)")
            {
                if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != null && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != "")
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName;
                else
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name;


            }

            // 0x2 (TYPE_CODE_ARRAY)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x2 (TYPE_CODE_ARRAY)")
            {
                Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName + "[]";
                Node2Tag.ImageIndex = 21;
                Node2Tag.SelectedImageIndex = Node2Tag.ImageIndex;
            }

            // 0x9 (TYPE_CODE_FLT)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x9 (TYPE_CODE_FLT)")
            {
                Node2Tag.Parent.Collapse();
                Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name + " " + ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.length + " bytes";

                Node2Tag.ImageIndex = 18;
                Node2Tag.SelectedImageIndex = Node2Tag.ImageIndex;

                if (((treeNodeSymbolTag)Node2Tag.Parent.Tag) != null)
                {
                    if (((treeNodeSymbolTag)(Node2Tag.Parent.Tag)).Dwarfstrings.code == "0x16 (TYPE_CODE_TYPEDEF)")
                        Node2Tag.Parent.ImageIndex = 18;

                    Node2Tag.Parent.SelectedImageIndex = Node2Tag.Parent.ImageIndex;
                }
            }



            // 0x4 (TYPE_CODE_UNION)
            else if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.code == "0x4 (TYPE_CODE_UNION)")
            {
                if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != null && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName != "")
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.parsedName;

                if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name != null && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name != "" && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name != "<NULL>")
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.name;

                if (((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname != null && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname != "" && ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname != "<NULL>")
                    Node2Tag.Text = ((treeNodeSymbolTag)Node2Tag.Tag).Dwarfstrings.tagname;

                if (Node2Tag.Text == null || Node2Tag.Text == "" || Node2Tag.Text.StartsWith("Un-Named"))
                    Node2Tag.Text = "Union";

                Node2Tag.ImageIndex = 22;
                Node2Tag.SelectedImageIndex = Node2Tag.ImageIndex;
            }
        }
        private uint ASCIIHex2_Value(string addrString)
        {
            uint addressFromString = 0x0;
            List<uint> charIntValues = new List<uint>();

            uint i = 0;
            uint tempInt = 0;



            foreach (char asciiChar in addrString.ToCharArray())
            {
                if (asciiChar == '0')
                    tempInt = 0;
                else if (asciiChar == '1')
                    tempInt = 1;
                else if (asciiChar == '2')
                    tempInt = 2;
                else if (asciiChar == '3')
                    tempInt = 3;
                else if (asciiChar == '4')
                    tempInt = 4;
                else if (asciiChar == '5')
                    tempInt = 5;
                else if (asciiChar == '6')
                    tempInt = 6;
                else if (asciiChar == '7')
                    tempInt = 7;
                else if (asciiChar == '8')
                    tempInt = 8;
                else if (asciiChar == '9')
                    tempInt = 9;
                else if (asciiChar == 'a')
                    tempInt = 10;
                else if (asciiChar == 'b')
                    tempInt = 11;
                else if (asciiChar == 'c')
                    tempInt = 12;
                else if (asciiChar == 'd')
                    tempInt = 13;
                else if (asciiChar == 'e')
                    tempInt = 14;
                else if (asciiChar == 'f')
                    tempInt = 15;

                charIntValues.Add(tempInt);
            }

            i = (uint)charIntValues.Count - 1;
            foreach (uint charValue in charIntValues)
            {
                uint tempVal = charValue * ((uint)Math.Pow(16, i));
                addressFromString += tempVal;
                i--;
            }

            return addressFromString;
        }
    }

    ///////////////////////////////////////////////
    // Class - treeNodeTag
    public class treeNodeTag
    {
        public string pathString;
        public string shortName;
        public bool isDirectory;
        public bool hasSubDirectories;
        public bool nodeProcessed;
    }

    ///////////////////////////////////////////////
    // Class - treeNodeSymbolTag
    public class treeNodeSymbolTag : treeNodeTag
    {
        public int nodeDepth;
        public int nodeLineNumber;
        public string addrstr;
        public uint addr;
        public DwarfTypeNodeDataClass Dwarfstrings = new DwarfTypeNodeDataClass();
        public string sourceCode;
        public bool isBaseType = false;
        public string longName;
    }

    ///////////////////////////////////////////////
    // Class - DwarfTypeNodeDataClass
    public class DwarfTypeNodeDataClass
    {
        public string childNodeHeader;
        public string parsedName;
        public string typeNode;
        public string name;
        public string tagname;
        public string code;
        public string length;
        public string objfile;
        public string targetType;
        public string pointerType = "";
        public string referenceType;
        public string typeChain;
        public string instanceFlags;
        public string flags;
        public string nfields;
    }

    public class FileNDirTools
    {
        public static void RDeleteOverNOver(string pathDir)
        {
            foreach (string fName in Directory.GetFiles(pathDir))
                File.Delete(fName);

            foreach (string dirName in Directory.GetDirectories(pathDir))
                RDeleteOverNOver(dirName);
        }
        public static void RecurrsiveDelete(string pathRoot)
        {
            string pathDirectory = pathRoot;

            if (Directory.Exists(pathDirectory))
            {
                foreach (string dirString in Directory.GetDirectories(pathDirectory))
                    RDeleteOverNOver(dirString);

                Directory.Delete(pathDirectory, true);
            }
        }
    }
    

    #endregion
}
