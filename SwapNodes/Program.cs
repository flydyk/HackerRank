using System;

namespace SwapNodes
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LoadAndSwapTree(new[]
                {
                    new[] {2, 3},
                    new[] {4, -1},
                    new[] {5, -1},
                    new[] {6, -1},
                    new[] {7, 8},
                    new[] {-1, 9},
                    new[] {-1, -1},
                    new[] {10, 11},
                    new[] {-1, -1},
                    new[] {-1, -1},
                    new[] {-1, -1},
                },
                new[] {2, 4});
        }

        private static int[][] LoadAndSwapTree(int[][] indexes, int[] queries)
        {
            var tree = new Tree<int>(new TreeNode<int> {Value = 1});

            LoadTree(tree, indexes);

            return SwapTree(tree, queries);
        }

        private static int[][] SwapTree(Tree<int> tree, int[] queries)
        {
            int[][] result = new int[queries.Length][];

            for (var i = 0; i < queries.Length; i++)
            {
                var query = queries[i];
                for (int q = query; q <= tree.Depth; q += query)
                {
                    var treeNodes = tree.GetNodes(q);
                    foreach (var treeNode in treeNodes)
                    {
                        var temp = treeNode.Left;
                        treeNode.Left = treeNode.Right;
                        treeNode.Right = temp;
                    }
                }

                result[i] = tree.Traverse().ToArray();
            }

            return result;
        }

        private static void LoadTree(Tree<int> tree, int[][] indexes)
        {
            TreeNode<int> node;
            for (var i = 0; i < indexes.Length; i++)
            {
                node = tree.Next();
                if (node == null)
                {
                    return;
                }

                node.SetLeftNode(indexes[i][0] != -1
                    ? new TreeNode<int> {Value = indexes[i][0]}
                    : TreeNode<int>.Null);

                node.SetRightNode(indexes[i][1] != -1
                    ? new TreeNode<int> {Value = indexes[i][1]}
                    : TreeNode<int>.Null);
            }
        }
    }
}
