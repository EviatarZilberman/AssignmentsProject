Stack<int> s = new Stack<int>();

for(int i = 0; i < 10; i++)
{
    s.Push(i);
}

for(int i = 0;i < 10; i++)
{
    Console.WriteLine(s.Pop());
}