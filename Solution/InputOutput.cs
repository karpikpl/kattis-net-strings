using System;
using System.Globalization;
using System.IO;

namespace KattisSolution.IO
{
    public class Tokenizer
    {
        private readonly StreamReader reader;
        private int pos;
        private string[] tokens = new string[0];

        public Tokenizer(Stream inStream)
        {
            var bs = new BufferedStream(inStream);
            reader = new StreamReader(bs);
        }

        public Tokenizer()
            : this(Console.OpenStandardInput())
        {
            // Nothing more to do
        }

        private string PeekNext()
        {
            if (pos < 0)
                // pos < 0 indicates that there are no more tokens
                return null;
            if (pos < tokens.Length)
            {
                if (tokens[pos].Length == 0)
                {
                    ++pos;
                    return PeekNext();
                }
                return tokens[pos];
            }
            var line = reader.ReadLine();
            if (line == null)
            {
                // There is no more data to read
                pos = -1;
                return null;
            }
            // Split the line that was read on white space characters
            tokens = line.Split(null);
            pos = 0;
            return PeekNext();
        }

        public bool HasNext()
        {
            return (PeekNext() != null);
        }

        public string Next()
        {
            var next = PeekNext();
            if (next == null)
                throw new NoMoreTokensException();
            ++pos;
            return next;
        }
    }

    public class Scanner : Tokenizer, IScanner
    {
        public Scanner(Stream inStream)
            : base(inStream)
        {
        }

        public int NextInt()
        {
            return int.Parse(Next());
        }

        public long NextLong()
        {
            return long.Parse(Next());
        }

        public float NextFloat()
        {
            return float.Parse(Next());
        }

        public double NextDouble()
        {
            return double.Parse(Next());
        }
    }

    public class NoMoreTokensException : Exception
    {
    }

    public interface IScanner
    {
        int NextInt();
        long NextLong();
        float NextFloat();
        double NextDouble();
        bool HasNext();
        string Next();
    }

    public class BufferedStdoutWriter : StreamWriter
    {
        public BufferedStdoutWriter(Stream outStream)
            : base(new BufferedStream(outStream))
        {
        }
    }

    public class OptimizedPositiveIntReader : IScanner
    {
        private readonly StreamReader _reader;

        public OptimizedPositiveIntReader(Stream inStream)
        {
            _reader = new StreamReader(inStream);
        }

        public int NextInt()
        {
            int c, result = 0;
            var isInt = false;
            while (!_reader.EndOfStream)
            {
                c = _reader.Read();

                while (c >= '0' && c <= '9')
                {
                    result = result * 10 + c - '0';
                    c = _reader.Read();
                    isInt = true;
                }

                if (isInt)
                    return result;
            }

            throw new NoMoreTokensException();
        }

        public long NextLong()
        {
            throw new NotImplementedException();
        }

        public float NextFloat()
        {
            throw new NotImplementedException();
        }

        public double NextDouble()
        {
            throw new NotImplementedException();
        }

        public bool HasNext()
        {
            throw new NotImplementedException();
        }

        public string Next()
        {
            throw new NotImplementedException();
        }
    }

    public class LineReader : IScanner
    {
        private readonly StreamReader _reader;

        public LineReader(Stream inStream)
        {
            _reader = new StreamReader(inStream);
        }

        public int NextInt()
        {
            return int.Parse(Next(), CultureInfo.InvariantCulture);
        }

        public long NextLong()
        {
            return long.Parse(Next(), CultureInfo.InvariantCulture);
        }

        public float NextFloat()
        {
            return float.Parse(Next(), CultureInfo.InvariantCulture);
        }

        public double NextDouble()
        {
            return double.Parse(Next(), CultureInfo.InvariantCulture);
        }

        public bool HasNext()
        {
            return !_reader.EndOfStream;
        }

        public string Next()
        {
            return _reader.ReadLine();
        }
    }
}