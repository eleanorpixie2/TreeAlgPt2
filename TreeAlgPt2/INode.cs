using System;
using System.Collections.Generic;
using System.Text;

namespace Tree_Pt2
{
    interface INode
    {
        string Id {get; set;}
        string Content { get; set; }
        bool IsReady { get; set; }
        int WhiteSpace { get; set; }

        List<Node> Children { get; set; }

        Node Parent { get; set; }
        int Depth { get; set; }

        string Get(string Id, bool shouldGetBranch);
        bool AddNode(Node toAdd, string parentId); //enforce unique on add
        bool DeleteNode(string objectId);
        bool MoveNode(string objectId, string parentId); // move all children/"the branch" as needed
        List<Node> FindNode(Node n); // might return list of matches 
        Node FindNode(string id);
    }
}
