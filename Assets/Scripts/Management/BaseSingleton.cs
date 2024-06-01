using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Это базовый класс для создания синглтонов.
 Синглтон - это объект, который существует в 
 единственном экземпляре во всей игре. 
 Это полезно для управления глобальными аспектами игры,
 такими как управление сценами или камерой.*/
public class BaseSingleton : Singleton<BaseSingleton>
{
    
}
