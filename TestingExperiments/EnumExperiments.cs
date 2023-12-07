using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumExperiments : MonoBehaviour
{
    public Seasons season;

   private void UpdateSeason(Seasons nextSeason)
    {
        season = nextSeason;

        switch (nextSeason)
        {
            case Seasons.spring:
                break;
            case Seasons.summer:
                break;
            case Seasons.autumn:
                break;
            case Seasons.winter:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Seasons
    {
        spring,
        summer,
        autumn,
        winter,
    }

}
