using System;
using System.IO;
using System.Text;
using KattisSolution.IO;

namespace KattisSolution
{
    public static class Extensions
    {
        public static void WriteToStream(this MemoryStream ms, string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            ms.Write(bytes, 0, bytes.Length);
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput(), (_) => { });
        }

        public static void Solve(Stream stdin, Stream stdout, Action<Func<string>> log = null)
        {
            var Log = log ?? ((msg) => Console.WriteLine(msg()));

            Log(() => "Solving joinstrings. https://open.kattis.com/problems/joinstrings");

            //IScanner scanner = new OptimizedPositiveIntReader(stdin);
            // uncomment when you need more advanced reader
            // IScanner scanner = new Scanner(stdin);
            IScanner scanner = new LineReader(stdin);
            var writer = new BufferedStdoutWriter(stdout);

            var n = scanner.NextInt();
            Log(() => $"Input: {n}");

            string[] s = new string[n];

            for (int i = 0; i < n; i++)
            {
                s[i] = scanner.Next();

                Log(() => $"{i}: {s[i]}");
            }

            Log(() => $"Input: {string.Join(",", s)}");
            int a = 0, b;

            MyLList[] solution = new MyLList[n];

            for (int i = 0; i < n - 1; i++)
            {
                var ops = scanner.Next().Split(new char[] { ' ' });

                // ops[0] -> .e. concatenate 𝑎𝑡ℎ string and 𝑏𝑡ℎ string and store the result in 𝑎𝑡ℎ string,
                a = int.Parse(ops[0]) - 1;
                b = int.Parse(ops[1]) - 1;

                if (solution[a] == null)
                {
                    solution[a] = new MyLList(a);
                }

                // b can be used only once!
                if (solution[b] == null)
                {
                    // b was never visited
                    solution[a].AddLast(b);
                    Log(() => $"{i}'th step. {a} {b} => {string.Join(",", solution[a])}");

                    // mark b empty
                    solution[b] = MyLList.Empty;
                    continue;
                }

                solution[a].Append(solution[b]);
                solution[b] = MyLList.Empty;

                Log(() => $"{i}'th step. {a} {b} => {string.Join(",", solution[a])}");
            }

            if (n == 1)
            {
                // edge case
                writer.Write($"{s[0]}\n");
                writer.Flush();
                return;
            }

            MyNode current = solution[a].First;
            while (current != null)
            {
                writer.Write(s[current.Value]);
                current = current.Next;
            }

            writer.Write("\n");
            writer.Flush();
        }

        public class MyLList
        {
            public static MyLList Empty = new MyLList();

            public bool IsEmpty => First == null;

            public MyNode First { get; set; }
            public MyNode Last { get; set; }

            public MyLList(int val)
            {
                First = new MyNode(val);
                Last = First;
            }

            private MyLList()
            {

            }

            public void AddLast(int val)
            {
                Last.Next = new MyNode(val);
                Last = Last.Next;
            }

            public void Append(MyLList list)
            {
                this.Last.Next = list.First;
                this.Last = list.Last;
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                var node = this.First;

                while (node != null)
                {
                    sb.Append(node.Value).Append(", ");
                    node = node.Next;
                }

                return sb.ToString();
            }
        }

        public class MyNode
        {
            public int Value { get; private set; }
            public MyNode Next { get; set; }

            public MyNode(int val)
            {
                this.Value = val;
            }
        }
    }
}