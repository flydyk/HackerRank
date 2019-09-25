using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SwapNodes
{
    [DebuggerDisplay("IsNull={IsNull}, Depth={Depth}, Value={Value}")]
    public class TreeNode : TreeNodeBase<int>
    {
    }

    public class Tree : TreeBase<int>
    {
    }

    [DebuggerDisplay("IsNull={IsNull}, Depth={Depth}, Value={Value}")]
    public class TreeNodeBase<T>
    {
        public static TreeNodeBase<T> Empty = new TreeNodeBase<T>(true);

        public TreeNodeBase()
            : this(false)
        {

        }

        private TreeNodeBase(bool isNull)
        {
            IsNull = isNull;
        }

        public bool IsNull { get; }

        public int Depth { get; private set; } = 1;

        public TreeBase<T> Tree { get; set; }
        public TreeNodeBase<T> Parent { get; set; }

        public TreeNodeBase<T> Left { get; private set; }
        public TreeNodeBase<T> Right { get; private set; }

        public T Value { get; set; }

        public TreeNodeBase<T> SetLeftNode(TreeNodeBase<T> node)
        {
            Left = node;
            return SetNode(Left);
        }

        public TreeNodeBase<T> SetRightNode(TreeNodeBase<T> node)
        {
            Right = node;
            return SetNode(Right);
        }

        private TreeNodeBase<T> SetNode(TreeNodeBase<T> node)
        {
            node.Tree = Tree;
            node.Parent = this;
            node.Depth = Depth + 1;

            Tree.AddNode(node);

            return node;
        }
    }

    public class TreeBase<T>
    {
        private TreeNodeBase<T> _root;
        private readonly Dictionary<int, List<TreeNodeBase<T>>> _nodes = new Dictionary<int, List<TreeNodeBase<T>>>();

        public TreeNodeBase<T> Root
        {
            get => _root;
            set
            {
                _root = value;
                _root.Tree = this;
            }
        }

        public TreeNodeBase<T> GetNodeByIndex(int depth, int index)
        {
            return _nodes[depth][index];
        }

        // for internal use only
        internal void AddNode(TreeNodeBase<T> node)
        {
            if (!_nodes.TryGetValue(node.Depth, out var nodes))
            {
                nodes = new List<TreeNodeBase<T>>();
                _nodes.Add(node.Depth, nodes);
            }

            nodes.Add(node);
        }

        private int _currentDepth;
        private int _nodeIndex;
        private int _currentIndex;

        public TreeNodeBase<T> Next()
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

                if (_currentDepth > _nodes.Count + 1)
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
