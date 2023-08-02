using Utilities;

List<int> list = new List<int>();
for (int i = 0; i < 10; i++)
{
    list.Add(i);
}

for (int i = list.Count;i > 0; i--)
{
    Console.WriteLine(list[i]);
}