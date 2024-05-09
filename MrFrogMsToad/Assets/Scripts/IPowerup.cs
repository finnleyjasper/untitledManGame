/*filename: IPowerup.cs
* author: Finnley
* description: an interface for powerups; all powerups must have a duration
*
*created: 09 May 2024
* last modified:  09 May 2024
*/

/* Notes:
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerup
{
    [SerializeField] public float Duration { get; }
}
