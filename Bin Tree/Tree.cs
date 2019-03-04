using System;

namespace Bin_Tree
{
    internal sealed class Tree<K, D> where K : IComparable<K>
    {
        internal int Length { get; private set; }//длина дерева
        internal Node Root;//указатель на корень
        internal int depth { get; private set; } //Глубина

        public class Node
        {
            public K Key { get; private set; }              //ключ объекта
            public D Data { get; private set; }             //значение в элементе
            public Node Pred { get; set; }          //предок
            public Node Left;          //указатель на левого сына
            public Node Right;         //указатель на правого сына

            public Node(K key, D data, Node pred)//конструктор с параметрами
            {
                Key = key;
                Data = data;
                Pred = pred;
                Left = Right = null;
            }

            public void Remove(ref Node sec)
            {
                Key = sec.Key;
                Data = sec.Data;
                if (sec.Right != null)
                {
                    if (sec.Pred.Right.Equals(sec))
                    {
                        sec.Pred.Right = sec.Right;
                    }
                    else
                    {
                        sec.Pred.Left = sec.Right;
                    }
                }
                sec = null;
            }
        }

        internal class Iterator//итератор
        {
            private Tree<K, D> tree;
            public Node Cur { get; set; }  //текущий элемент

            public Iterator(Tree<K, D> tree)//конструктор с параметрами
            {
                this.tree = tree;
                Cur = tree.Root;
            }
        }

        internal Tree()//конструктор без параметров
        {
            Length = 0;
            depth = 0;
            Root = null;
        }

        internal void Clear()//очистка дерева
        {
            Length = 0;
            Root = null;
        }

        internal void Add(K key, D data)//Метод добавления узла
        {
            add_support(key, data, ref Root, null);
            return;
        }

        //private void add_support(K key, D data, Node node, Node pred)//Дополнительный метод для добавления узла
        //{
        //    if (node.Key.CompareTo(key) > 0)
        //    {
        //        if (node.Left == null)
        //            node.Left = new Node(key, data, node);
        //        else
        //            add_support(key, data, node.Left, node);
        //    }
        //    else
        //    {
        //        if (node.Right == null)
        //            node.Right = new Node(key, data, node);
        //        else
        //            add_support(key, data, node.Right, node);
        //    }
        //}

        private void add_support(K key, D data, ref Node node, Node pred)//Дополнительный метод для добавления узла
        {
            if (node == null)
            {
                node = new Node(key, data, pred);
                Length++;
            }
            else
            {
                if (node.Key.CompareTo(key) > 0)
                {
                    add_support(key, data, ref node.Left, node);
                }
                else
                {
                    if (node.Key.CompareTo(key) != 0)
                        add_support(key, data, ref node.Right, node);
                    else;
                }
            }
            return;
        }

        internal void Remove(K key)//Метод удаления узла
        {
            if (Root == null)
                return;
            ref Node node = ref search_support(key, ref Root, ref Root);
            ref Node ch = ref remove_support(ref node.Right, ref node, node);
            if (ch.Equals(node) && node.Left != null)
            {
                node.Left.Pred = node.Pred;
                node = node.Left;
            }
            else
                node.Remove(ref ch);
            Length--;
            return;
        }

        private ref Node remove_support(ref Node node, ref Node pred, Node root)//Дополнительный метод для удаления узла
        {
            if (node == null)
                return ref pred;
            node = ref remove_support(ref node.Left, ref node, root);
            return ref node;
        }

        internal D Search(K key)// Метод для поиска 
        {
            return search_support(key, ref Root, ref Root).Data;
        }

        private ref Node search_support(K key, ref Node node, ref Node pred)//Дополнительный метод для поиска 
        {
            if (node == null || node.Key.CompareTo(key) == 0)
                return ref node;
            if (key.CompareTo(node.Key) < 0)
                node = ref search_support(key, ref node.Left, ref node);
            if (key.CompareTo(node.Key) > 0)
                node = ref search_support(key, ref node.Right, ref node);
            return ref node;
        }

        internal int Depth()
        {
            depth_support(Root, 1);
            return depth;
        }

        private void depth_support(Node node, int d)
        {
            if (node == null)
                return;
            if (depth < d)
                depth = d;
            depth_support(node.Left, d + 1);
            depth_support(node.Right, d + 1);
            return;
        }
    }
}
