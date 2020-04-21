using System.Linq;
using System.Collections;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Cylenium
{
    public class Elements : IList<Element>
    {
        private IList<IWebElement> _list;

        public List<Element> Items { get; }

        public int Count => ((ICollection<Element>)Items).Count;

        public Elements(By by, IList<IWebElement> elements)
        {
            _list = elements;
            Items = new List<Element>();
            foreach (var e in elements)
            {
                Items.Add(new Element(by, e));
            }
        }

        public Element First()
        {
            return Items.First();
        }

        public Element Last()
        {
            return Items.Last();
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

# region IList members

        public bool IsReadOnly => ((ICollection<Element>)Items).IsReadOnly;

        public Element this[int index]
        {
            get => ((IList<Element>)Items)[index];
            set => ((IList<Element>)Items)[index] = value;
        }

        public int IndexOf(Element item)
        {
            return ((IList<Element>)Items).IndexOf(item);
        }

        public void Insert(int index, Element item)
        {
            ((IList<Element>)Items).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Element>)Items).RemoveAt(index);
        }

        public void Add(Element item)
        {
            ((ICollection<Element>)Items).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Element>)Items).Clear();
        }

        public bool Contains(Element item)
        {
            return ((ICollection<Element>)Items).Contains(item);
        }

        public void CopyTo(Element[] array, int arrayIndex)
        {
            ((ICollection<Element>)Items).CopyTo(array, arrayIndex);
        }

        public bool Remove(Element item)
        {
            return ((ICollection<Element>)Items).Remove(item);
        }

        public IEnumerator<Element> GetEnumerator()
        {
            return ((IEnumerable<Element>)Items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Items).GetEnumerator();
        }

# endregion
    }
}
