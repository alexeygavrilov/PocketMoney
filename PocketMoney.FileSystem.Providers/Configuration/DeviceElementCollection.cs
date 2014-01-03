using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PocketMoney.FileSystem.Configuration
{
    [ConfigurationCollection(typeof (DeviceElement), AddItemName = "device")]
    public sealed class DeviceElementCollection : ConfigurationElementCollection, ICollection<DeviceElement>
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return
                    ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public DeviceElement this[int index]
        {
            get { return (DeviceElement) BaseGet(index); }
        }

        public new DeviceElement this[string name]
        {
            get { return (DeviceElement) BaseGet(name); }
        }

        #region ICollection<DeviceElement> Members

        public void Add(DeviceElement item)
        {
            BaseAdd(item);
        }

        public void Clear()
        {
            BaseClear();
        }

        public bool Contains(DeviceElement item)
        {
            return item != null && this.Any(device => device.Name.Equals(item.Name));
        }

        public void CopyTo(DeviceElement[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public new bool IsReadOnly
        {
            get { return true; }
        }

        bool ICollection<DeviceElement>.Remove(DeviceElement item)
        {
            if (Contains(item))
            {
                Remove(item);
                return true;
            }
            return false;
        }

        public new IEnumerator<DeviceElement> GetEnumerator()
        {
            return new DeviceElementEnumerator(this);
        }

        #endregion

        protected override ConfigurationElement CreateNewElement()
        {
            return new DeviceElement();
        }

        protected override object GetElementKey(
            ConfigurationElement element)
        {
            return ((DeviceElement) element).Name;
        }

        public int IndexOf(DeviceElement action)
        {
            return BaseIndexOf(action);
        }

        public void Remove(DeviceElement action)
        {
            if (action != null &&
                BaseIndexOf(action) >= 0)
            {
                BaseRemove(action.Name);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        #region Nested type: DeviceElementEnumerator

        private class DeviceElementEnumerator : IEnumerator<DeviceElement>
        {
            private readonly DeviceElementCollection _coll;
            private DeviceElement _current;
            private int _index;

            public DeviceElementEnumerator(DeviceElementCollection coll)
            {
                _index = -1;
                _coll = coll;
            }

            #region IEnumerator<DeviceElement> Members

            public DeviceElement Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (++_index >= _coll.Count)
                {
                    return false;
                }
                _current = _coll[_index];
                return true;
            }

            public void Reset()
            {
                _current = default(DeviceElement);
                _index = 0;
            }

            public void Dispose()
            {
                _current = default(DeviceElement);
                _index = _coll.Count;
            }

            #endregion
        }

        #endregion
    }
}