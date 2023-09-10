using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class OpenAllLevel : MonoBehaviour
    {
       [SerializeField] private List<LoadingLevel> _levels;

       public void OpenAllLevels(){
            for(int i=0;i<_levels.Count;i++){
                _levels[i].OpenLevel();
            }
       }

       public void CloseAllLevels(){
            for(int i=0;i<_levels.Count;i++){
                _levels[i].CloseLevel();
            }
       }
    }
}
