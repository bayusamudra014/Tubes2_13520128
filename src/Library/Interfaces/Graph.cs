namespace PathFinder.Interfaces
    // template buat fungsi template graph, buat nyimpen data node dan juga posisinya
    // saat ini lagi dimana
{
    public interface Graph<T>
    {
        Node<T> getAdjacent(Node<T> parent, int idx);
        Node<T> addValue(T value);
        Node<T> findNode(T value);
        Node<T> addAdjacent(Node<T> node, Node<T> adj);
    }
}
