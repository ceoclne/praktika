using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсач
{
    public class HashTable
    {
        public Dictionary<int, List<HashNode>> GetBuckets()
        {
            return buckets;
        }
        // Здесь будут храниться только существующие корзины
        private Dictionary<int, List<HashNode>> buckets =
            new Dictionary<int, List<HashNode>>();

        // Количество операций
        public int Operations = 0;

        // Количество коллизий
        public int Collisions = 0;

        // Количество элементов
        public int CountElements()
        {
            return buckets.Sum(x => x.Value.Count);
        }

        // Количество корзин
        public int CountBuckets()
        {
            return buckets.Count;
        }

        // Получение номера корзины
        public int GetBucket(string key)
        {
            return Math.Abs(key.GetHashCode()) % 20;
        }

        // Добавление
        public void Add(string key, string value)
        {
            int bucket = GetBucket(key);

            if (!buckets.ContainsKey(bucket))
                buckets.Add(bucket, new List<HashNode>());

            else
                Collisions++;

            buckets[bucket].Add(new HashNode(key, value));

            Operations++;
        }

        // Поиск
        public HashNode Find(string key)
        {
            int bucket = GetBucket(key);

            if (!buckets.ContainsKey(bucket))
                return null;

            foreach (HashNode node in buckets[bucket])
            {
                if (node.Key == key)
                    return node;
            }

            return null;
        }

        // Изменение
        public bool Update(string key, string value)
        {
            HashNode node = Find(key);

            if (node == null)
                return false;

            node.Value = value;

            Operations++;

            return true;
        }

        // Удаление
        public bool Remove(string key)
        {
            int bucket = GetBucket(key);

            if (!buckets.ContainsKey(bucket))
                return false;

            HashNode node = Find(key);

            if (node == null)
                return false;

            buckets[bucket].Remove(node);

            if (buckets[bucket].Count == 0)
                buckets.Remove(bucket);

            Operations++;

            return true;
        }

        // Очистка
        public void Clear()
        {
            buckets.Clear();

            Operations++;

            Collisions = 0;
        }

     

        // Максимальная цепочка
        public int MaxChain()
        {
            if (buckets.Count == 0)
                return 0;

            return buckets.Max(x => x.Value.Count);
        }

        // Средняя длина цепочки
        public double AverageChain()
        {
            if (buckets.Count == 0)
                return 0;

            return buckets.Average(x => x.Value.Count);
        }

        // Коэффициент загрузки
        public double LoadFactor()
        {
            if (buckets.Count == 0)
                return 0;

            return (double)CountElements() / CountBuckets();
        }
    }
}
