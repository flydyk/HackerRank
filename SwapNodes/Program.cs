using System;

namespace SwapNodes
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var tree = new Tree();
            tree.Root = new TreeNode();
            LoadTree(tree, new[]
                {
                    new []{2,3},
                    new []{4,-1},
                    new []{5,-1},
                    new []{6,-1},
                    new []{7,8},
                    new []{-1,9},
                    new []{-1,-1},
                    new []{10,11},
                    new []{-1,-1},
                    new []{-1,-1},
                    new []{-1,-1},
                },
                null);
        }

        private static void LoadTree(Tree tree, int[][] indexes, int[] queries)
        {
            TreeNodeBase<int> node;
            for (var i = 0; i < indexes.Length; i++)
            {
                node = tree.Next();
                if (node == null)
                {
                    return;
                }

                node.SetLeftNode(indexes[i][0] != -1
                    ? new TreeNode {Value = indexes[i][0]}
                    : TreeNode.Empty);

                node.SetRightNode(indexes[i][1] != -1
                    ? new TreeNode {Value = indexes[i][1]}
                    : TreeNode.Empty);
            }
        }
    }
}
