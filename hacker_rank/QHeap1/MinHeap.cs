namespace QHeap1;

public class MinHeap
{
    private IList<int> values = new List<int>();

    public int Minimum => values[0];

    public void Add(int val)
    {
        values.Add(val);
        SortHeap(values.Count - 1);
    }

    public void Delete(int val)
    {
        var index = values.IndexOf(val);

        if (index == -1)
        {
            return;
        }

        values.Remove(val);
        SortHeap(index);
    }

    private void SortHeap(int sortIndex)
    {
        if (sortIndex == 0)
        {
            return;
        }

        if (values.Count == 0 || values.Count == 1)
        {
            return;
        }

        int parentIndex;
        switch (sortIndex % 2)
        {
            case 0:
                parentIndex = (sortIndex / 2) - 1;
                break;
            case 1:
                parentIndex = ((sortIndex + 1) / 2) - 1;
                break;
            default:
                throw new NotImplementedException();
        }

        var val = values[sortIndex];
        var parentValue = values[parentIndex];

        // Switch the parent with the value
        if (val == Math.Min(parentValue, val))
        {
            values[sortIndex - 1] = parentValue;
            values[parentIndex] = val;
        }

        SortHeap(parentIndex);

        // Is there a more efficient way? Maybe using binary AND operator?
        // var height = (int)Math.Floor(Math.Log2(count));
        // var maxCount = (int)Math.Pow(2, height) - 1;
    }

    private int getHeight()
    {
        var count = values.Count;
        if (count == 0)
        {
            return 0;
        }

        var result = 1;
        while ((count >> 1) != 1)
        {
            result += 1;
        }

        return result;
    }
}
