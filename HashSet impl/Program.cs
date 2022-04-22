using System;
using System.Collections.Generic;

namespace HashSet_impl
{
  class Program
  {
    static void Main(string[] args)
    {
      MyHashSet_usingList hash = new MyHashSet_usingList();
      hash.Add(1);
      hash.Add(2);
      Console.WriteLine(hash.Contains(1));
      Console.WriteLine(hash.Contains(3));
      hash.Add(2);
      Console.WriteLine(hash.Contains(2));
      hash.Remove(2);
      Console.WriteLine(hash.Contains(2));
    }
  }

  public class ListNode
  {
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
      this.val = val;
      this.next = next;
    }
  }
  public class MyHashSet
  {
    List<ListNode> list;
    public MyHashSet()
    {
      // We are taking 100 buckets initially
      list = new List<ListNode>(100);
      for (int i = 0; i < 100; i++)
      {
        // assign all buckets as null to mark nothing is added in a bucket
        list.Add(null);
      }
    }

    public void Add(int key)
    {
      // get the bucket index
      int index = key % 100;
      // if the bucket is null, which means this key not yet added
      if (list[index] == null)
      {
        list[index] = new ListNode(key, null);
      }
      else
      {
        // if the bucket has value , check key is already present in the bucket
        // take the reference in head, head will be used to reassign the values to current bucket.
        ListNode head = list[index];
        // traverse the entire bucket, look for the key is present  on the bucket
        // if next element is null, which means you have reached end of the list, else check current node value matching with key ?
        // if matched which menas the element is existing and we dont add the same key again
        while (list[index].next != null && list[index].val != key)
        {
          list[index] = list[index].next;
        }
        // last element not having the key ?
        if (list[index].next == null && list[index].val != key)
        {
          // add the new key at the end of the bucket
          list[index].next = new ListNode(key, null);
        }
        // re initialize the bucket
        list[index] = head;
      }
    }

    public void Remove(int key)
    {
      if (Contains(key))
      {
        int index = key % 100;
        ListNode head = list[index];
        ListNode prev = null;
        while (list[index].val != key)
        {
          prev = list[index];
          list[index] = list[index].next;
        }
        if (prev != null)
        {
          prev.next = list[index].next;
        }
        else
        {
          head = head.next;
        }
        list[index] = head;

      }

    }

    public bool Contains(int key)
    {
      int index = key % 100;
      if (list[index] == null)
      {
        return false;
      }
      else
      {
        ListNode head = list[index];
        while (head != null && head.val != key)
        {
          head = head.next;
        }
        if (head == null) { return false; }
        return true;
      }
    }
  }

  public class MyHashSet_usingList
  {
    List<List<int>> parent;
    public MyHashSet_usingList()
    {
      // We are taking 100 buckets initially
      parent = new List<List<int>>(100);
      for (int i = 0; i < 100; i++)
      {
        // assign all buckets as null to mark nothing is added in a bucket
        parent.Add(null);
      }
    }

    public void Add(int key)
    {
      if (Contains(key)) return;
      int index = key % 100;
      var child = parent[index];
      if (child == null)
      {
        child = new List<int>();
        child.Add(key);
      }
      else
      {
        child.Add(key);
      }
      parent[index] = child;
    }

    public void Remove(int key)
    {
      if (Contains(key))
      {
        int index = key % 100;
        var child = parent[index];
        child.Remove(key);
      }
    }

    public bool Contains(int key)
    {
      int index = key % 100;
      var child = parent[index];
      if (child == null) return false;
      else
      {
        return child.Contains(key);
      }
    }
  }
}
