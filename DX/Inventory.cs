﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DX
{
    public class Inventory
    {
        Item[] items;
        Player player;
        int size = 24;
        int width=4;
        int height = 6;

        public Inventory(Item[] _items,Player _player) {
            items = _items;
            player = _player;
        }


        public Inventory(Player _player)
        {
            items = new Item[Size];
            player = _player;
        }


        public void Activate(int Invid)
        {
            Task.Factory.StartNew(() =>
            {
                items[Invid].WorkFunc(player);
                if (items[Invid].QuantityLowCheck())
                {
                    items[Invid] = null;
                }
            });
        }

        public void Add(Item item) {
            if (item.Quantity == 0) return;
            for (int i = 0; i < Size; i++)
            {
                if(items[i] == null)continue;
                if (items[i].Id == item.Id&&items[i].Quantity!=items[i].MaxQuantity) {
                    int quantity = items[i].Quantity;
                    items[i].Quantity += item.Quantity;
                    if (items[i].QuantityHighCheck())
                    {
                        int maxQuantity = items[i].MaxQuantity;
                        item.Quantity -= maxQuantity - quantity;
                        items[i].Quantity = maxQuantity;
                        this.Add(item);
                    }
                    return;
                }
            }

            for (int i = 0; i < Size; i++)
            {
                if (items[i] == null)
                {
                        items[i] = (Item)item.Clone();
                        if (items[i].QuantityHighCheck())
                        {
                            int maxQuantity = items[i].MaxQuantity;
                            item.Quantity -= maxQuantity;
                            items[i].Quantity = maxQuantity;
                            this.Add(item);
                        }
                        return;
                    
                }
            }
            item.DropItem(player.X,player.Y);
        }

        public void Delete(int Invid) {
            items[Invid] = null;
        }


        public void Drop(int Invid)
        {
            items[Invid].DropItem(player.X,player.Y);
            items[Invid] = null;
        }

        public void DropOne(int Invid) {
            Item drop = (Item)items[Invid].Clone();
            drop.Quantity = 1;
            drop.DropItem(player.X, player.Y);
            items[Invid].Quantity -=1;
            if (items[Invid].QuantityLowCheck()) items[Invid] = null;
        }

        public Item[] Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        public int Size
        {
            get
            {
                return Height*width;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }
    }
}