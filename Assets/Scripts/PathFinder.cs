using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    public static List<Grid.Position> FindPath(Tile[,] tiles, Grid.Position fromPosition, Grid.Position toPosition)
    {
        PathNode node = BreadthFirstSearch(tiles, fromPosition, toPosition);
        var path = new List<Grid.Position>();

        path.Reverse();
        return path;
	}

    public class PathNode
    {
        public Grid.Position position;
        public PathNode parent;

        public PathNode(Grid.Position position, PathNode parent)
        {
            this.position = position;
            this.parent = parent;
        }
    }

    private static PathNode BreadthFirstSearch(Tile[,] tiles, Grid.Position fromPosition, Grid.Position toPosition)
    {
        HashSet<PathNode> nodes = new HashSet<PathNode>();
        var queue = new Queue<PathNode>();

        var root = new PathNode(fromPosition, null);

        nodes.Add(root);
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            PathNode node = queue.Dequeue();
            if (node.position.x == toPosition.x && node.position.y == toPosition.y)
            {
                Debug.Log("ACHEI O CARA");
                return node;
            }
            else
            {
                TryEnqueueNode(tiles, queue, nodes, node, new Grid.Position(node.position.x + 1, node.position.y));
                TryEnqueueNode(tiles, queue, nodes, node, new Grid.Position(node.position.x - 1, node.position.y));
                TryEnqueueNode(tiles, queue, nodes, node, new Grid.Position(node.position.x, node.position.y + 1));
                TryEnqueueNode(tiles, queue, nodes, node, new Grid.Position(node.position.x, node.position.y - 1));
            }
        }
        return null;
    }

    private static void TryEnqueueNode(Tile[,] tiles, Queue<PathNode> queue, HashSet<PathNode> nodes, PathNode currentNode, Grid.Position position)
    {
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);


        if(position.x > width || position.x < 0 || position.y > height || position.y < 0)
        {
            Debug.Log("Não está na grade");
            return;
        }
        else
        {
            var node = new PathNode(position, currentNode);
            nodes.Add(node);
            queue.Enqueue(node);

        }
    }
}
