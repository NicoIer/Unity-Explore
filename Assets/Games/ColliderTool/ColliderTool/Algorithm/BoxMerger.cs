using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColliderTool
{
    public static class BoxMerger
    {
        /// <summary>
        /// 把2D的网格地图合并成多个Box
        /// TODO 这里可以不用Set 直接左下角和右上角两个点就能确定一个2D Box
        /// </summary>
        /// <returns></returns>
        public static List<HashSet<Vector2Int>> Merge(HashSet<Vector2Int> points)
        {
            List<HashSet<Vector2Int>> result = new List<HashSet<Vector2Int>>(points.Count / 2); // 估算一下会用到的内存

            bool GetRect2D(Vector2Int bottomLeft, out HashSet<Vector2Int> rect)
            {
                if (!points.Contains(bottomLeft))
                {
                    rect = null;
                    return false;
                }

                int w = 0;
                int h = 0;
                rect = new HashSet<Vector2Int>();

                // 搜索以bottomLeft为左下角的矩形
                int maxLoop = points.Count;
                int currentLoop = 0;
                while (currentLoop++ < maxLoop)
                {
                    w += 1;
                    h += 1;
                    //检查是否能够满足当前的矩形
                    Vector2Int topLeft = bottomLeft + new Vector2Int(0, h);
                    if (!points.Contains(topLeft))
                    {
                        break;
                    }

                    Vector2Int topRight = bottomLeft + new Vector2Int(w, h);
                    if (!points.Contains(topRight))
                    {
                        break;
                    }

                    Vector2Int bottomRight = bottomLeft + new Vector2Int(w, 0);
                    if (!points.Contains(bottomRight))
                    {
                        break;
                    }

                    //从topLeft -> topRight -> bottomRight 只要路径上的点都在 那就是一个矩形 否则break return false

                    //topLeft -> topRight
                    for (int i = topLeft.x; i < topRight.x; i++)
                    {
                        Vector2Int p = new Vector2Int(i, topLeft.y);
                        if (!points.Contains(p))
                        {
                            goto end;
                        }
                    }

                    //topRight -> bottomRight
                    for (int i = topRight.y; i > bottomRight.y; i--)
                    {
                        Vector2Int p = new Vector2Int(topRight.x, i);
                        if (!points.Contains(p))
                        {
                            goto end;
                        }
                    }

                    //路径都OK 继续迭代
                }

                end: ;

                //根据 w 和 h 把点加入 顺便把点移除掉 避免重复计算
                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        Vector2Int p = bottomLeft + new Vector2Int(i, j);
                        rect.Add(p);
                        points.Remove(p);
                    }
                }

                return true;
            }

            while (points.Count > 0)
            {
                //排序后取左下角第一个点
                Vector2Int bottomLeft = points.OrderBy(p => p.x).ThenBy(p => p.y).First();
                // Debug.Log($"bottomLeft:{bottomLeft}");
                if (GetRect2D(bottomLeft, out HashSet<Vector2Int> rect))
                {
                    result.Add(rect);
                }
            }


            return result;
        }

        public static Bounds ConvertTo(HashSet<Vector2Int> input, Vector3 size, float y)
        {
            //找到左下角和右上角就行
            Vector2Int bottomLeft = input.OrderBy(p => p.x).ThenBy(p => p.y).First();
            Vector2Int topRight = input.OrderByDescending(p => p.x).ThenByDescending(p => p.y).First();
            Vector3 center = new Vector3((bottomLeft.x + topRight.x) / 2f, y, (bottomLeft.y + topRight.y) / 2f);
            Vector3 size3D = new Vector3(size.x * (topRight.x - bottomLeft.x + 1), size.y, size.z * (topRight.y - bottomLeft.y + 1));
            return new Bounds(center, size3D);
        }
    }
}