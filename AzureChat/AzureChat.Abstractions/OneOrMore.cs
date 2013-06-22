using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Abstractions
{
    public struct OneOrMore<T> : IEnumerable<T>
    {
        public OneOrMore(T one)
        {
            _One = one;
            _More = null;
            _Type = OneOrMoreType.One;
        }

        public OneOrMore(IEnumerable<T> more)
        {
            _One = default(T);
            _More = more;
            _Type = OneOrMoreType.More;
        }

        T _One;

        public T One
        {
            get { return _One; }

        }
        IEnumerable<T> _More;

        public IEnumerable<T> More
        {
            get { return _More; }

        }

        OneOrMoreType _Type;

        public OneOrMoreType Type
        {
            get { return _Type; }

        }

        public IEnumerator<T> GetEnumerator()
        {
            var v = this.One;
            switch (Type)
            {
                case OneOrMoreType.One:

                    return new SingleEnumerator<T>(One);

                case OneOrMoreType.More:
                    return More.GetEnumerator();

                default:
                    return Enumerable.Empty<T>().GetEnumerator();

            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    struct SingleEnumerator<T> : IEnumerator<T>
    {
        bool _Done;
        T _Current;
        public SingleEnumerator(T value)
        {
            _Current = value;
            _Done = false;

        }


        public T Current
        {
            get { return _Current; }
        }

        public void Dispose()
        {
            _Current = default(T);
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            var rval = !_Done;
            _Done = true;
            return rval;
        }

        public void Reset()
        {
            _Done = false;
        }
    }




    public enum OneOrMoreType
    {
        One,
        More

    }
}
