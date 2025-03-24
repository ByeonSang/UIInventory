using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Deque<T>
{
    private T[] deque;
    private int front = 0;
    private int rear = 0;
    public int Capacity { get; private set; } = 0;

    /// <summary>
    /// 미리 크기 조절
    /// </summary>
    /// <param name="Capacity"></param>
    public Deque(int capacity)
    {
        Capacity = capacity;
        deque = new T[capacity];
    }

    public void AddFront(T value)
    {
        if(Is_Full())
        {
            Debug.LogWarning("Deque is Full!!!");
            return;
        }    

        front = (Capacity + (front - 1)) % Capacity;
        deque[front] = value;
    }

    public bool TryDeleteFront(out T outResult)
    {
        if(Is_Empty())
        {
            Debug.LogWarning("Deque is Empty!!!");
            outResult = default(T);
            return false;
        }

        int result = front;
        front = (front + 1) % Capacity;
        outResult = deque[result];
        return true;
    }

    public void AddRear(T value)
    {
        if (Is_Full())
        {
            Debug.LogWarning("Deque is Full!!!");
            return;
        }

        rear = (rear + 1) % Capacity;
        deque[rear] = value;
    }

    public bool TryDeleteRear(out T outResult)
    {
        if (Is_Empty())
        {
            Debug.LogWarning("Deque is Empty!!!");
            outResult = default(T);
            return false;
        }

        int result = rear;
        rear = (rear - 1) % Capacity;
        outResult = deque[result];
        return true;
    }

    public bool TryPeekFront(out T result)
    {
        if(Is_Empty())
        {
            Debug.LogWarning($"Deque Is Empty!!!");
            result = default(T);
            return false;
        }
        
        result = deque[front];

        return true;
    }

    public bool TryPeekRear(out T result)
    {
        if (Is_Empty())
        {
            Debug.LogWarning($"Deque Is Empty!!!");
            result = default(T);
            return false;
        }

        result = deque[rear];

        return true;
    }

    // 예외사항 만들기
    public bool Is_Empty()
    {
        return front == rear;
    }

    public bool Is_Full()
    {
        return ((rear + 1) % Capacity == front) || (Capacity + (front - 1) % Capacity == rear);
    }

    public void Clear()
    {
        front = rear = 0;
    }

}
