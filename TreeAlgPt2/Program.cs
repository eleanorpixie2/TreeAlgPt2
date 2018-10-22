using System;
using System.Collections.Generic;

namespace Tree_Pt2
{
    class Program
    {
        static Tree test = new Tree();
        static void Main(string[] args)
        {

            test.Start();
            GettingNode();
        }

        static void Menu()
        {
            //bool keepGoing=true;
            //while(keepGoing)
            //{
           //}
        }

        static void GettingNode()
        {
            Console.WriteLine("Enter value of Node you want to find: ");

            string nodeValue = Console.ReadLine();
            bool getBranch=false;
            while (true)
            {
                Console.WriteLine("Do you want the branch with it? Y/N?");

                string getbranch = Console.ReadLine();

                if(getbranch.Equals("Y")|| getbranch.Equals("y"))
                {
                    getBranch = true;
                    break;
                }
                else if (getbranch.Equals("N") || getbranch.Equals("n"))
                {
                    getBranch = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter valid option");
                    continue;
                }
            }

            Console.WriteLine(test.root.Get(FindNodes(nodeValue), getBranch));
        }

        static void MovingNode()
        {
            Console.WriteLine("Enter the value of the object you wish to move:");
            string nodeValue = Console.ReadLine();
            Node nodeToMove = test.root.FindNode(FindNodes(nodeValue));

            Console.WriteLine("Enter the value of the object you wish to move the other " +
                "object to:");
            string parentValue = Console.ReadLine();

            Node nodeToMoveTo = test.root.FindNode(FindNodes(parentValue));

            test.root.MoveNode(nodeToMove.Id, nodeToMoveTo.Id);
        }

        static void AddNodeByUser()
        {
            Console.WriteLine("Enter value you want to add: ");
            string value = Console.ReadLine();
            Console.WriteLine("Enter parent node value: ");
            string parent = Console.ReadLine();

            Node nParent = test.root.FindNode(FindNodes(parent));
            Node temp = new Node(nParent.WhiteSpace+8, null, value);
            nParent.AddNode(temp, nParent.Id);
        }

        static void RemoveNode()
        {
            Console.WriteLine("Enter vaule you want deleted:");
            string value = Console.ReadLine();

            test.root.DeleteNode(FindNodes(value));
        }

        static string FindNodes(string value)
        {
            Node temp = new Node(0, null, value);
            List<Node> n = test.root.FindNode(temp);
            if (n != null)
            {
                if (n.Count > 1)
                {
                    foreach (Node node in n)
                    {
                        Console.WriteLine(node.Id + " " + node.Content);
                        if (node.Children.Count > 0)
                        {
                            foreach (Node child in node.Children)
                            {
                                Console.WriteLine("\t" + child.Id + " " + child.Content);
                            }
                        }
                    }

                    Console.WriteLine("enter id of node you want to change");
                    string id = Console.ReadLine();
                    return id;
                }
                else if(n.Count==1)
                {
                    return n[0].Id;
                }
            }
            else
            {
                Console.WriteLine("Can't find value");
            }
            return null;
        }
    }
}
