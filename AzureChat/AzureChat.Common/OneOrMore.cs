using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureChat.Common
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

                    return Single (One);

                case OneOrMoreType.More:
                    return More.GetEnumerator();

                default:
                    return Enumerable.Empty<T>().GetEnumerator();

            }
        }

        IEnumerator<T> Single(T value)
        {
            yield return value;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }





    public enum OneOrMoreType
    {
        One,
        More

    }
}
