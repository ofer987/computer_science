/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class ListNode
{
    public int Value { get; set; }
    public ListNode Next { get; set; }

    public ListNode(int x)
    {
        Value = x;
    }
}

public class Solution
{
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        var nodeList1 = l1;
        var nodeList2 = l2;

        ListNode head = null;
        ListNode next = null;

        var setNext = (next) => {
            if (nodeList2 == null)
            {
                var nextNode = new ListNode(nodeList1.Value);
                next = nextNode;

                nodeList1 = nodeList1.Next;
            }
            else if (nodeList1 == null)
            {
                var nextNode = new ListNode(nodeList2.Value);
                next = nextNode;

                nodeList2 = nodeList2.Next;
            }
            else if (nodeList1.Value < nodeList2.Value)
            {
                var nextNode = new ListNode(nodeList1.Value);
                next = nextNode;

                nodeList1 = nodeList1.Next;
            }
            else
            {
                var nextNode = new ListNode(nodeList2.Value);
                next = nextNode;

                nodeList2 = nodeList2.Next;
            }
        };

        setNext(head);
        var next = head;

        while (nodeList1 != null && nodeList2 != null)
        {
            setNext(next);

            next = next.Next;
        }

        return head;
    }
}
