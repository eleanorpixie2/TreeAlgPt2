using System;
using System.Collections.Generic;
using System.Text;

namespace Tree_Pt2
{
    class Node : INode
    {
        #region Properties 
        public string Id
        {
            get => _Id;

            set
            {
                if (value != null)
                {
                    _Id = value;
                }
                else
                {
                    _Id = Guid.NewGuid().ToString();
                }
            }
        }

        private string _Id;

        public string Content
        {
            get => _Content;
            set { _Content = value; }
        }

        private string _Content;

        public bool IsReady
        {
            get;
            set;
        }
        public List<Node> Children
        {
            get { return _children; }
            set { _children = value; }
        }
        private List<Node> _children = new List<Node>();

        public Node Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        private Node _parent;

        public int Depth
        {
            get => _depth;
            set
            {
                _depth = value;
            }
        }
        private int _depth;

        public int WhiteSpace
        {
            get => _wspace;
            set { _wspace = value; }
        }
        private int _wspace;
        #endregion

        public Node(int _whitespace, string _id, string _content)
        {
            WhiteSpace = _whitespace;
            Id = _id;
            Content = _content;
        }

        public bool AddNode(Node toAdd, string parentId)
        {
            Node parent = FindNode(parentId);

            return parent.AddNode(toAdd);
        }

        public bool AddNode(Node toAdd)
        {
            toAdd.Depth = toAdd.WhiteSpace / 8;

            if (toAdd.Depth < this.Depth)
            {
                this.Parent.AddNode(toAdd);
            }
            else if (toAdd.Depth == this.Depth)
            {
                this.Parent.AddNode(toAdd);
            }
            else
            {
                toAdd.Parent = this;
                this.Children.Add(toAdd);
            }

            return true;
        }

        public bool DeleteNode(string objectId)
        {
            Node toBeDeleted = FindNode(objectId);
            Node parent = toBeDeleted.Parent;

            return parent.Children.Remove(toBeDeleted);
        }

        public List<Node> FindNode(Node n)
        {
            List<Node> matches = new List<Node>();

            if (Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    if (!child.Content.Equals(n.Content))
                    {
                        matches.AddRange(child.FindNode(n));
                    }
                    else
                    {
                        matches.Add(child);
                    }
                }
            }

            return matches;
        }
        Node temp = null;
        public Node FindNode(string id)
        {
            
            if (Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    if (!child.Id.Equals(id))
                    {
                        child.FindNode(id);
                    }
                    else
                    {
                        temp = child;
                        return temp;
                    }
                }
            }
            else
            {
                if (this.Id.Equals(id))
                {
                    return this;
                }

            }

            return temp;
        }

        public string Get(string id, bool shouldGetBranch)
        {
            string retrievedNodes = "";

            if (shouldGetBranch)
            {
                Node found = FindNode(id);
                retrievedNodes += "\n";

                for(int i =0; i<found.Depth;i++)
                {
                    retrievedNodes += "\t";
                }

                retrievedNodes += (found.Content);

                if (found.Children.Count <= 0)
                    return retrievedNodes;

                foreach(Node n in found.Children)
                {
                    retrievedNodes += found.Get(n.Id, shouldGetBranch);
                }
            }
            else
            {
                retrievedNodes += FindNode(id).Content;
            }

            return retrievedNodes;
        }

        public bool MoveNode(string objectId, string parentId)
        {
            Node toBeMoved = FindNode(objectId);
            Node whereToBeMoved = FindNode(parentId);

            if (toBeMoved == null || whereToBeMoved == null)
            {
                return false;
            }

            toBeMoved.Parent.Children.Remove(toBeMoved);
            whereToBeMoved.Children.Add(toBeMoved);

            return true;
        }
    }
}
