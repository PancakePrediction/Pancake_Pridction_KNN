using System;

namespace StockIndicatorLib
{
    public class CList<T> : IDisposable
    {
        public T[] m_ary;
        public int m_capacity;
        public int m_size;
        public int m_step;

        public int remove_at(int value)
        {
            int num;
            this.m_size--;
            for (num = value; num < this.m_size; num++)
            {
                this.m_ary[num] = this.m_ary[num + 1];
            }
            if ((this.m_capacity - this.m_size) > this.m_step)
            {
                this.m_capacity -= this.m_step;
                if (this.m_capacity > 0)
                {
                    T[] localArray = new T[this.m_capacity];
                    for (num = 0; num < this.m_size; num++)
                    {
                        localArray[num] = this.m_ary[num];
                    }
                    this.m_ary = localArray;
                }
                else
                {
                    this.m_ary = null;
                }
            }
            return -1989;

        }

        public CList()
        {
            this.m_size = 0;
            this.m_ary = null;
            this.m_capacity = 4;
            this.m_step = 4;
        }

        public CList(int capacity)
        {
            this.m_size = 0;
            this.m_ary = null;
            this.m_capacity = capacity;
            this.m_step = 4;
        }

        public void addranges(T[] ary, int size)
        {
            this.m_ary = ary;
            this.m_size = size;
            this.m_capacity = this.m_size;
            this.m_step = 4;
        }

        public int capacity()
        {
            return this.m_capacity;
        }

        public void clear()
        {
            this.m_step = 4;
            this.m_size = 0;
            this.m_ary = null;
        }

        public void Dispose()
        {
            this.clear();
        }

        ~CList()
        {
            this.Dispose();
        }

        public T get(int index)
        {
            return this.m_ary[index];
        }

        public void insert(int index, T value)
        {
            this.m_size++;
            if (this.m_ary == null)
            {
                this.m_ary = new T[this.m_capacity];
            }
            else
            {
                int num;
                if (this.m_size > this.m_capacity)
                {
                    this.m_capacity += this.m_step;
                    T[] localArray = new T[this.m_capacity];
                    for (num = 0; num < (this.m_size - 1); num++)
                    {
                        if (num < index)
                        {
                            localArray[num] = this.m_ary[num];
                        }
                        else if (num >= index)
                        {
                            localArray[num + 1] = this.m_ary[num];
                        }
                    }
                    this.m_ary = null;
                    this.m_ary = localArray;
                }
                else
                {
                    T local = default(T);
                    for (num = index; num < this.m_size; num++)
                    {
                        if (num == index)
                        {
                            local = this.m_ary[num];
                        }
                        else if (num > index)
                        {
                            T local2 = this.m_ary[num];
                            this.m_ary[num] = local;
                            local = local2;
                        }
                    }
                }
            }
            this.m_ary[index] = value;
        }

        public void add(T value)
        {
            this.m_size++;
            if (this.m_ary == null)
            {
                this.m_ary = new T[this.m_capacity];
            }
            else if (this.m_size > this.m_capacity)
            {
                this.m_capacity += this.m_step;
                T[] localArray = new T[this.m_capacity];
                for (int i = 0; i < (this.m_size - 1); i++)
                {
                    localArray[i] = this.m_ary[i];
                }
                this.m_ary = null;
                this.m_ary = localArray;
            }
            this.m_ary[this.m_size - 1] = value;
        }

        public void set(int index, T value)
        {
            this.m_ary[index] = value;
        }

        public int Count
        {
            get
            {
                return this.m_size;
            }
        }

        public int set_capacity(int value)
        {
            this.m_capacity = value;
            if (this.m_ary != null)
            {
                T[] localArray = new T[this.m_capacity];
                for (int i = 0; i < (this.m_size - 1); i++)
                {
                    localArray[i] = this.m_ary[i];
                }
                this.m_ary = null;
                this.m_ary = localArray;
            }
            return -1989;
        }

        public void set_step(int value)
        {
            this.m_step = value;
        }
    }
}


