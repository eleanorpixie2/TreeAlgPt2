﻿using System;
using System.Collections.Generic;

namespace Tree_Pt2
{
    class Program
    {
        //tree object
        static Tree test = new Tree();
        static void Main(string[] args)
        {
            //call the starting menu for intializing tree
            StartMenu();
            //call main menu for editing the tree
            Menu();
        }

        static void StartMenu()
        {
            //menu option choice
            int choice = 0;
            while (true)
            {
                Console.WriteLine("Please choose how you want to create a tree: \n1-Read in from file\n2-Create tree from input");
                string sChoice = Console.ReadLine();
                //convert user input into an int
                try
                {
                    choice = Convert.ToInt32(sChoice);
                    //make sure number entered is a menu option
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
            //call method based on user menu choice
            switch(choice)
            {
                case 1:
                    GetFile();
                    break;

                case 2:
                    CreateTreeUser();
                    break;
            }
        }

        //get filename for input
        static void GetFile()
        {
            string notFound = "";
            while (notFound != null)
            {
                Console.WriteLine(@"Enter file name and extenision for file in c:\workspace\: ");
                string path = Console.ReadLine();
                notFound = test.Start(path);
                if (notFound != null)
                {
                    Console.WriteLine(notFound);
                }
            }
        }

        //Main menu for editing tree
        static void Menu()
        {
            //bool that keeps the menu going
            bool keepGoing = true;
            //menu choice
            int choice = 0;
            while (keepGoing)
            {
                while (true)
                {
                    Console.WriteLine("Please choose an option: \n1-Move a node\n2-Add a node\n3-Delete a node\n4-Get a node\n5-Write tree to file\n6-Exit");
                    string sChoice = Console.ReadLine();
                    //convert user input to int
                    try
                    {
                        choice = Convert.ToInt32(sChoice);
                        //make sure choice is a valid menu option
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

                //call function based on user choice
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

        //create a tree solely from user input
        static void CreateTreeUser()
        {
            //create intial root node
            test.StartUser();
            //temporary holding variable
            Node tempNode = null;
            string value = "";
            //press f to exit loop
            Console.WriteLine("Enter F for the node value when you are finished entering nodes.");
            while (value != "f" || value != "f")
            {
                Console.WriteLine("Enter node value: ");
                value = Console.ReadLine();
                //check if f was entered
                if(value == "f" || value == "f")
                {
                    break;
                }
                int depth = 0;
                //get depth of node
                while (true)
                {
                    Console.WriteLine("Enter the node's depth(i.e. 0,1,2,etc): ");
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
                //make new node
                Node n = new Node(8 * depth, null, value);
                //if there is no whitespace, then add as child to root node
                if (n.WhiteSpace == 0)
                {
                    tempNode = test.root;
                }
                //add node to parent node
                tempNode.AddNode(n);
                //keep reference of last node added
                tempNode = n;
            }
            
        }

        //get a node
        static void GettingNode()
        {
            Console.WriteLine("Enter value of Node you want to find: ");

            string nodeValue = Console.ReadLine();
            bool getBranch=false;
            //get whether the user just wants the node or the whole branch
            while (true)
            {
                Console.WriteLine("Do you want the branch with it? Y/N?");

                string getbranch = Console.ReadLine();

                //set bool based on user input
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

            string id = FindNodes(nodeValue);

            if (id != null)
                //Display node value
                Console.WriteLine(test.root.Get(id, getBranch));
        }

        //move a node in the tree
        static void MovingNode()
        {
            Node nodeToMoveTo;

            Console.WriteLine("Enter the value of the object you wish to move:");
            string nodeValue = Console.ReadLine();
            //find the node we want to move
            Node nodeToMove = test.root.FindNode(FindNodes(nodeValue));

            Console.WriteLine("Enter the value of the object you wish to move the other " +
                "object to:");
            string parentValue = Console.ReadLine();
            if (parentValue.Equals(test.root.Content))
            {
                //find the object we want to move the value to
                nodeToMoveTo = test.root;
            }
            else
            {
                //find the object we want to move the value to
                nodeToMoveTo = test.root.FindNode(FindNodes(parentValue));
            }

            //pass the id values to the node function
            test.root.MoveNode(nodeToMove.Id, nodeToMoveTo.Id);
        }

        //add a node somewhere in the tree
        static void AddNodeByUser()
        {
            //get the node the user wants to add
            Console.WriteLine("Enter value you want to add: ");
            string value = Console.ReadLine();
            //get node that the user wants to add a node to
            Console.WriteLine("Enter parent node value: ");
            string parent = Console.ReadLine();

            //get the parent node
            Node nParent = test.root.FindNode(FindNodes(parent));
            //create new node
            Node temp = new Node(nParent.WhiteSpace+8, null, value);
            //add new node as child of the parent node
            nParent.AddNode(temp, nParent.Id);
        }

        //delete a node from tree
        static void RemoveNode()
        {
            //get the value you want to delete
            Console.WriteLine("Enter vaule you want deleted:");
            string value = Console.ReadLine();

            //remove the node from tree
            test.root.DeleteNode(FindNodes(value));
        }

        //Finds a node and returns id value, if there are duplicates it deals with that
        static string FindNodes(string value)
        {
            //temporary node variable
            Node temp = new Node(0, null, value);
            //finds all nodes with the passed in value
            List<Node> n = test.root.FindNode(temp);
            if (n.Count != 0)
            {
                //if there is more than one object with the same value
                if (n.Count > 1)
                {
                    //write out each object with it's unique id and children
                    foreach (Node node in n)
                    {
                        Console.WriteLine(node.Id + " " + node.Content);
                        if (node.Children.Count > 0)
                        {
                            foreach (Node child in node.Children)
                            {
                                Console.WriteLine("\t" + child.Content);
                            }
                        }
                    }

                    //get which one the user wants to edit
                    Console.WriteLine("enter id of node you want to change");
                    string id = Console.ReadLine();
                    return id;
                }
                //if there is only one object with that value, return the unique id
                else if(n.Count==1)
                {
                    return n[0].Id;
                }
            }
            //if the list is null, then the value couldn't be found;
            else
            {
                Console.WriteLine("Can't find value");
                return null;
            }
            return null;
        }
    }
}
