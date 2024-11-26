using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public enum AchievementId {
    None,
    DumAss,
    SmartAss,
    YouCanOpenDoors,
    Room2DoorOpen,
    Puzzle1,
    Pilar1,
    Pilar2,
    Pilar3,
    Pilar4,
    PilarAllDoor,
    Room4Puzzle1,
    Room4Puzzle2,
    DragonFire,
    DragonSolve,
    TheEnd
}

public enum AchievementGroup {
    COMMON,
    Group_1,
}

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private AchievementObserver achievementObserver;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text AchievementTitle;
    [SerializeField] private TMP_Text AchievementDescription;
    [SerializeField] private float animationOffset = 20f;
    [SerializeField] private AudioSource audioSource;

    private List<Achievement> achievements;
    private AchievementId current = AchievementId.None;

    private HashSet<AchievementId> unlocked;
    private HashSet<AchievementGroup> groups;

    private RectTransform _rectTransform;
    private Vector2 _originalPanelPos;

    private void Awake()
    {
        _rectTransform = panel.GetComponent<RectTransform>();
        _originalPanelPos = _rectTransform.anchoredPosition;
        achievementObserver.OnAchievementEarned += Achieved;
    }

    public AchievementManager(){
        this.unlocked = new HashSet<AchievementId>();
        this.groups = new HashSet<AchievementGroup>();
        this.InitializeAchievements();
    }
    
    private void InitializeAchievements(){
        if (achievements != null){
            return;
        }

        this.achievements = new List<Achievement>();

        /* Room 1 */
        this.achievements.Add(new Achievement(AchievementId.DumAss, "Dum Ass!", "Congratulations, you are certified", AchievementGroup.Group_1));
        this.achievements.Add(new Achievement(AchievementId.SmartAss, "Smart Ass!", "Congratulations, you are certified", AchievementGroup.Group_1));
        this.achievements.Add(new Achievement(AchievementId.YouCanOpenDoors, "Door Opener!", "Congratulations, you can open doors"));

        /* Room 2 */      
        this.achievements.Add(new Achievement(AchievementId.Room2DoorOpen, "Hard Truth", "Do we even have free will?"));

        /* Room 3 */
        this.achievements.Add(new Achievement(AchievementId.Pilar1, "Pilar of Death", "In this world, nothing can be certain, except death and taxes.\nBenjamin Franklin"));
        this.achievements.Add(new Achievement(AchievementId.Pilar2, "Pilar of Purple", "Yes the color purple deserves a pilar"));
        this.achievements.Add(new Achievement(AchievementId.Pilar3, "Pilar of Placeholder", "Our writes are terrible, can even find an name for the third pilar"));
        this.achievements.Add(new Achievement(AchievementId.Pilar4, "Pilar of Gods", "Do you think God gets stoned? I think so . . . look at the platypus."));
        this.achievements.Add(new Achievement(AchievementId.PilarAllDoor, "Pilar master", "Did somebody order a cleaning crew"));

        /* Room 4 */
        this.achievements.Add(new Achievement(AchievementId.Room4Puzzle1, "Puzzle Maniac", "Even our developers couldn't solve this one"));
        this.achievements.Add(new Achievement(AchievementId.Room4Puzzle2, "Puzzle God", "Marry me?"));

        /* Room 5 */
        this.achievements.Add(new Achievement(AchievementId.DragonFire, "Flames of hell", "Wonder how much it costs"));
        this.achievements.Add(new Achievement(AchievementId.DragonSolve, "Dragon Lord", "Roses are red, violets are blue, my name is dave, microwave"));

        /* Room 6 */
        this.achievements.Add(new Achievement(AchievementId.TheEnd, "FREEDOMMMM", "Was this all just a game?"));

    }

    public void Achieved(AchievementId id){
        if (id == AchievementId.None)
        {
            Debug.Log("Achievement is none");
            return;
        }
        
        if(this.unlocked.Contains(id)){
            Debug.Log("Achievement already achieved");
            return;
        }

        Achievement achievement = FindAchievementById(id);
        if(achievement is null){
            Debug.Log("Achievement with id: {achievement.id} doesn't exist");
            return;
        }

        if(achievement.group != AchievementGroup.COMMON  && this.groups.Contains(achievement.group)){
            Debug.Log("Achievement from this group was already achieved");
            return;
        }

        this.groups.Add(achievement.group);
        this.current = id;
        this.unlocked.Add(achievement.id);
        
        StartCoroutine(Run(achievement.title, achievement.description));
    }
    
    private Achievement FindAchievementById(AchievementId id){
        return this.achievements.Find(ach => ach.id == id);
    }

    private IEnumerator Run(string title, string description)
    {
        Open();
        SetContent(title, description);
        playSound();

        yield return new WaitForSeconds(5);
        Close();

        yield return new WaitForSeconds(5);
        Empty();
    }

    private void Open(){
        _rectTransform.DOAnchorPos(new Vector2(_originalPanelPos.x, -_originalPanelPos.y - animationOffset), 1);
    }

    private void Close(){
        _rectTransform.DOAnchorPos(new Vector2(_originalPanelPos.x, _originalPanelPos.y), 1);
    }

    private void Empty(){
        this.AchievementTitle.text = "";
        this.AchievementDescription.text = "";
    }

    private void SetContent(string title, string description){
        this.AchievementTitle.text = title;
        this.AchievementDescription.text = description;
    }

    private void playSound(){
        if(audioSource){
            audioSource.Play();
        }
    }
}

public class Achievement {
    public AchievementId id;
    public string title;
    public string description;
    public AchievementGroup group;

    public Achievement(AchievementId id, string title, string description, AchievementGroup? group = null){
        this.id = id;
        this.title = title;
        this.description = description;
        this.group = (group is null) ?  AchievementGroup.COMMON : (AchievementGroup)group;
    }
}
