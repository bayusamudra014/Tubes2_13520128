using System;
using System.Collections.Generic;

namespace PathFinder.Interfaces
// template buat graph, buat nyimpen data node dan juga posisinya
// saat ini lagi dimana
{
    public interface Graph<T>
    {
        Tree<T> getAdjacent(TreeNode<T> parent, int idx);
        Tree<T> addValue(T value);
        Tree<T> findNode(T value);
        Tree<T> addAdjacent(TreeNode<T> node, TreeNode<T> adj);
    }
}