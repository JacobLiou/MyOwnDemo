public class Csharp12Demo(int id, string name)
{
    public int Id { get; set; } = id;

    public string name { get; set; } = name;

    public void SetArray(string ass)
    {
        int[] arr = [1, 2, 3, 4, 5];
        int[][] twod = [[1, 2, 3], [4, 5, 6]];

        //展开运算符
        int[] aaa = [.. arr];

        //lambda 默认参数 有些特性很方便
        var Increment = (int ss, int c = 1) => ss + c;
        Increment(1);
        Increment(1, 3);
    }

    public override string ToString() => $"{id} -- {name}";
}