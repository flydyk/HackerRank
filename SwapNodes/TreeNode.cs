using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SwapNodes
{
    [DebuggerDisplay("IsNull={IsNull}, Depth={Depth}, Value={Value}")]
    public class TreeNode<T>
    {
        public static TreeNode<T> Null = new TreeNode<T>(true);

        public TreeNode()
            : this(false)
        {

        }

        private TreeNode(bool isNull)
        {
            IsNull = isNull;
        }

        public bool IsNull { get; }

        public int Depth { get; private set; } = 1;

        public Tree<T> Tree { get; set; }
        public TreeNode<T> Parent { get; set; }

        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }

        public T Value { get; set; }

        public TreeNode<T> SetLeftNode(TreeNode<T> node)
        {
            Left = node;
            return SetNode(Left);
        }

        public TreeNode<T> SetRightNode(TreeNode<T> node)
        {
            Right = node;
            return SetNode(Right);
        }

        private TreeNode<T> SetNode(TreeNode<T> node)
        {
            node.Tree = Tree;
            node.Parent = this;
            node.Depth = Depth + 1;

            Tree.AddNode(node);

            return node;
        }
    }

    public class Tree<T>
    {
        private TreeNode<T> _root;
        private readonly Dictionary<int, List<TreeNode<T>>> _nodes = new Dictionary<int, List<TreeNode<T>>>();

        public TreeNode<T> Root
        {
            get => _root;
            private set
            {
                _root = value;
                _root.Tree = this;
                AddNode(_root);
            }
        }

        public int Depth => _nodes.Count;

        public List<TreeNode<T>> GetNodes(int depth)
        {
            return _nodes[depth];
        }

        // for internal use only
        internal void AddNode(TreeNode<T> node)
        {
            if (!_nodes.TryGetValue(node.Depth, out var nodes))
            {
                nodes = new List<TreeNode<T>>();
                _nodes.Add(node.Depth, nodes);
            }

            nodes.Add(node);
        }

        public Tree(TreeNode<T> root)
        {
            Root = root;
        }

        private int _currentDepth;
        private int _nodeIndex;
        private int _currentIndex;

        public List<T> Traverse()
        {
            var list = new List<T>();

            Traverse(Root, list);

            return list;
        }

        private void Traverse(TreeNode<T> node, List<T> list)
        {
            if (node.IsNull)
            {
                return;
            }

            Traverse(node.Left, list);

            list.Add(node.Value);

            Traverse(node.Right, list);
        }

        public TreeNode<T> Next()
        {
            while (true)
            {
                _nodeIndex++;
                _currentIndex++;
                if (_nodeIndex > Math.Pow(2, _currentDepth) - 1)
                {
                    _currentDepth++;
                    _currentIndex = 0;
                }

                if (_currentDepth == 1)
                {
                    return Root;
                }

                if (_currentDepth > Depth)
                {
                    return null;
                }

                var nodes = _nodes[_currentDepth];

                if (_currentIndex < nodes.Count && !nodes[_currentIndex].IsNull)
                {
                    return nodes[_currentIndex];
                }
            }
        }
    }
}
