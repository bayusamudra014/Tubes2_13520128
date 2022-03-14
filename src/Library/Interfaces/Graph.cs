using System;
using System.Collections.Generic;

// namespace PathFinder.Interfaces
// template buat graph, buat nyimpen data node dan juga posisinya
// saat ini lagi dimana
// {
// public interface Graph<T>
// {
// Node<T> getAdjacent(Node<T> parent, int idx);
// Node<T> addValue(T value);
// Node<T> findNode(T value);
//  Node<T> addAdjacent(Node<T> node, Node<T> adj);
// }
// }

// bikin data struktur buat tree nya

public class TreeNode<T> {

    private T value;

    private bool hasParent;


    private List<TreeNode<T>> children;

    public TreeNode(T value) { 
        if (value == null) {
            throw new ArgumentNullException("Cannot insert null value!");
        }
        this.value = value;
        this.children = new List<TreeNode<T>>();


}
