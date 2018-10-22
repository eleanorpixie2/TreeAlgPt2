using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tree_Pt2
{
    class Tree
    {
        //empty Root Node
        public Node root;

        //Holds a reference of the most recently added Node
        private Node tempNode;

        //Holds a list of all the siblings needed to be added at the same depth
        private List<Node> ToBeAdded = new List<Node>();

        public void Start()
        {
            root = new Node(0, null, null);
            root.Depth = -1;
            LoadContent();
        }

        private bool LoadContent()
        {
            using (StreamReader sr = new StreamReader(@"C:\workspace\people.txt"))
            {
                string line;
                int count = 0;
                int parentCount = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    for (int j = 0; j <= line.Length; j++)
                    {
                        //TO:DO parsing ID, Content
                        //this is a check for depth
                        if (line[j].Equals(' '))
                        {
                            count++;
                        }
                        else if (line[j].Equals('\t'))
                        {
                            count += 8; //1 tab = 8 empty spaces
                        }
                        else
                        {
                            break; //breaks out of for loop thats checking each char
                        }
                    }

                    if (parentCount == 0) // if it's count of whitespace was 0 then add set the tempNode to the root
                    {
                        tempNode = root;
                    }

                    if (count != parentCount)
                    {
                        AddNodesToTree(ToBeAdded); //add nodes in list to the tree

                        if (root.Children[0] != null)
                        {
                            tempNode = ToBeAdded[ToBeAdded.Count - 1]; //keep a reference of the last node added
                        }
                        ToBeAdded.Clear(); //clear the list
                    }

                    line = line.TrimStart('\t');
                    AddNodeToList(count, ToBeAdded, line); // add node to the list with its info

                    parentCount = count;
                    count = 0; //reset whitespace counter

                }//end of While

                AddNodesToTree(ToBeAdded);//Add last of the nodes
                                               //check for IO errors and other exceptions
                return true;

            }//end of StreamReader
        }//end of LoadContent()

        private void AddNodeToList(int count, List<Node> toBeAdded, string line)
        {
            ToBeAdded.Add(new Node(count, null, line));
        }

        private void AddNodesToTree(List<Node> toBeAdded)
        {
            foreach (var sib in toBeAdded)
            {
                tempNode.AddNode(sib);
            }
        }
    }
}

