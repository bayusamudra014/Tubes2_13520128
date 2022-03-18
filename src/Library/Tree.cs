using System;
using System.Collections.Generic;


namespace PathFinder
{
    public class Tree<T>
    {

        private TreeNode<T> root;
        public Tree(T value) {

            if (value == null) {

                throw new ArgumentNullException("Cannot insert null value!");
        
            }

            this.root = new TreeNode<T>(value);
    
        }
        
        public Tree(T value, params Tree<T>[] children) : this(value)
        {

            foreach (Tree<T> child in children)
            {

                this.root.AddChild(child.root);

            }
        }

        public TreeNode<T> Root {

            get {
            
                return this.root;
        
            }
    
        }

    }
}