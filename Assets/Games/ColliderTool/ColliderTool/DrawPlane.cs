using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColliderTool
{
    /// <summary>
    /// 绘制平面 记录了当前的绘制信息
    /// </summary>
    [DisallowMultipleComponent]
    public class DrawPlane : SceneSingleton<DrawPlane>
    {
        public GridContainer container { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            container = new GridContainer();
        }
    }

    public class GridContainer : IEnumerable<Grid>
    {
        private readonly HashSet<Grid> _filledGrids;
        public HashSet<Grid> tmpGrids { get; private set; }

        public GridContainer(int capacity = 2500)
        {
            _filledGrids = new HashSet<Grid>(capacity);
            tmpGrids = new HashSet<Grid>(capacity / 2);
        }

        public bool Contains(Grid grid)
        {
            return _filledGrids.Contains(grid);
        }

        public bool Contains(Vector3Int pos)
        {
            Grid grid = new Grid
            {
                pos = pos
            };
            return _filledGrids.Contains(grid);
        }

        public bool Contains(Vector3 pos)
        {
            Grid grid = new Grid(pos, Vector3.zero);
            return _filledGrids.Contains(grid);
        }

        public void Remove(Grid grid)
        {
            _filledGrids.Remove(grid);
        }

        public void Remove(Vector3Int pos)
        {
            Grid grid = new Grid
            {
                pos = pos
            };
            _filledGrids.Remove(grid);
        }

        public void Remove(Vector3 pos)
        {
            Grid grid = new Grid(pos, Vector3.zero);
            _filledGrids.Remove(grid);
        }

        public void Add(Grid grid)
        {
            _filledGrids.Add(grid);
        }

        public IEnumerator<Grid> GetEnumerator()
        {
            return _filledGrids.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void SaveTmpGrids()
        {
            _filledGrids.UnionWith(tmpGrids);
            tmpGrids.Clear();
        }
    }
}