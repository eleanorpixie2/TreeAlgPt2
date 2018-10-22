using System;
using System.Collections.Generic;

namespace Tree_Pt2
{
    class Program
    {
        static Tree test = new Tree();
        static void Main(string[] args)
        {

            StartMenu();
            Menu();
        }

        static void StartMenu()
        {
            int choice = 0;
            while (true)
            {
                Console.WriteLine("Please choose how you want to create a tree: \n1-Read in from file\n2-Create tree from input");
                string sChoice = Console.ReadLine();
                try
                {
                    choice = Convert.ToInt32(sChoice);
                    if(choice==1||choice==2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid menu option");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }

            switch(choice)
            {
                case 1:
                    test.Start();
                    break;

                case 2:
                    CreateTreeUser();
                    break;
            }
        }

        static void Menu()
        {
            bool keepGoing = true;
            int choice = 0;
            while (keepGoing)
            {
                while (true)
                {
                    Console.WriteLine("Please choose an option: \n1-Move a node\n2-Add a node\n3-Delete a node\n4-Get a node\n5-Write tree to file\n6-Exit");
                    string sChoice = Console.ReadLine();
                    try
                    {
                        choice = Convert.ToInt32(sChoice);
                        if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter valid menu option");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter a valid number.");
                    }
                }

                switch (choice)
                {
                    case 1:
                        MovingNode();
                        break;

                    case 2:
                        AddNodeByUser();
                        break;
                    case 3:
                        RemoveNode();
                        Console.WriteLine("Done");
                        break;
                    case 4:
                        GettingNode();
                        break;
                    case 5:
                        test.OutputToFile(test.root);
                        Console.WriteLine("Done");
                        break;
                    case 6:
                        keepGoing = false;
                        break;
                }
            }
        }

        static void CreateTreeUser()
        {
            test.StartUser();
            Node tempNode = null;
            string value = "";
            Console.WriteLine("Enter F for the node value when you are finished entering nodes.");
            while (value != "f" || value != "f")
            {
                Console.WriteLine("Enter node value: ");
                value = Console.ReadLine();
                if(value == "f" || value == "f")
                {
                    break;
                }
                int depth = 0;
                while (true)
                {
                    Console.WriteLine("Enter the node's depth(i.e. 0,1,2,etc: ");
                    string dValue = Console.ReadLine();
                    try
                    {
                        depth = Convert.ToInt32(dValue);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter valid number: ");
                    }
                }
                Node n = new Node(8 * depth, null, value);
                if (n.WhiteSpace == 0)
                {
                    tempNode = test.root;
                }
                tempNode.AddNode(n);
                tempNode = n;
            }
            
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
