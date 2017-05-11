using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuItem : MonoBehaviour
{
    [Flags]
    public enum ItemState
    {
        NEUTRAL = 1 << 0,
        SELECT = 1 << 2,
        HOVER = 1 << 3
    };

    public readonly Color[] itemColors =
    {
        Color.white,
        Color.green,
        Color.blue
    };

    private ItemState mState;
    private Material mMaterial;

    public ItemState State
    {
        get
        {
            return mState;
        }

        set
        {
            switch(value)
            {
                case ItemState.NEUTRAL:
                    mState = (mState & ~ItemState.SELECT) & ItemState.NEUTRAL;
                    OnSelect();
                    break;
                case ItemState.SELECT:
                    mState = (mState & ~ItemState.NEUTRAL) & ItemState.SELECT;
                    OnDeselect();
                    break;
                case ItemState.HOVER:
                    mState = mState | ItemState.HOVER;
                    break;
            }
        }
    }

    public abstract void OnSelect();
    public abstract void OnDeselect();

    private void Start()
    {
        mState = ItemState.NEUTRAL;
        mMaterial = GetComponent<TextMesh>().GetComponent<Material>();
    }

    private void Update()
    {
        if ((mState & ItemState.NEUTRAL) == ItemState.NEUTRAL)
        {
            mMaterial.color = itemColors[0];
        }
        if ((mState & ItemState.SELECT) == ItemState.SELECT)
        {
            mMaterial.color = itemColors[1];
        }
        if ((mState & ItemState.HOVER) == ItemState.HOVER)
        {
            mMaterial.color = itemColors[2];
        }
    }
}