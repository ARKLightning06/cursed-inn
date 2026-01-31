using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public enum NPCName {TestNPC, PohnJork, SirBorro, Gastou, OctaviousTheFifth, MayorThadworn, Wyl, Harry, Lupal}

public class NPCStats : MonoBehaviour
{
    [Header("Interactions")]
    public Player player;
    public UIManager uiManager;
    public InventoryManager inventoryManager;
    public Animator animator;

    [Header("Stats")]
    public NPCName character; // this is the character corresponding to this script, used for initializing dialogue stuff
    public string npcName;
    public int health;
    public bool isFighter;
    public int damage;
    public bool hasAnimation;
    public GameObject associatedItem;
    public bool associatedItemGiven = false;
    // attack type or smth somehow needs to go here...

    [Header("Dialogue")]
    public Sprite charVisualization;
    public Sprite playerVisualization;
    public string playerName;
    public Dialogue defaultDialogue;
    //public List<DialogueList> dialogueLists = new List<DialogueList>();
    public DialogueHandler handler;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // defaultDialogue = new Dialogue(defaultDialogue, "Default Dialogue", "Hello there!", true, true);
        // DialogueList tempStarter = new DialogueList();
        // tempStarter.dialogueOptions.Add(defaultDialogue);
        // handler = new DialogueHandler(defaultDialogue, dialogueLists, uiManager, charVisualization, "Nobody");
        animator = this.gameObject.GetComponent<Animator>();
        if(character == NPCName.TestNPC) // change back to == later!!!
        {
            //outline: first node is NPC saying Hello There! second node is Player saying General Kenobi, third node is NPC saying what's 9 + 10, fourth node is Player saying 19 or 21, 21 goes to NPC node saying wrong you uncultured swine try again, then back to fourth node, 21 is NPC saying good job, bye, then ending
            DialogueNode n1 = new DialogueNode(new List<Dialogue>(), false); // (NPC) hello there
            DialogueNode n2 = new DialogueNode(new List<Dialogue>(), true); // (Player) General Kenobi
            DialogueNode n3 = new DialogueNode(new List<Dialogue>(), false); // (NPC) What's 9 + 10? (options: 19, 21, in NEXT NODE)
            DialogueNode n4 = new DialogueNode(new List<Dialogue>(), true); // (Player) options: 19 or 21, 19 to n5, 21 to n6
            DialogueNode n5 = new DialogueNode(new List<Dialogue>(), false); // (NPC) 19 clicked, no you uncultured swine, try again, back to n3
            DialogueNode n6 = new DialogueNode(new List<Dialogue>(), false); // (NPC) 21 clicked, Good boyyyy. Good bye!
            DialogueNode end = new DialogueNode(new List<Dialogue>(), false); // empty node, end node to end dialogue 

            Dialogue d1 = new Dialogue(n2, "Hello There!", true); // stored in n1
            Dialogue d2 = new Dialogue(n3, "General Kenobi", true); // stored in n2
            Dialogue d3 = new Dialogue(n4, "What's 9 + 10?", true); // stored in n3
            Dialogue d4 = new Dialogue(n5, "19", true); // stored in n4
            Dialogue d5 = new Dialogue(n6, "21", true); // also stored in n4
            Dialogue d6 = new Dialogue(n3, "No you uncultured swine! Try again.", true); // stored in n5
            Dialogue d7 = new Dialogue(end, "Good boyyyy. Good bye!", true); // stored in n6

            n1.AppendDialogue(d1);
            n2.AppendDialogue(d2);
            n3.AppendDialogue(d3);
            n4.AppendDialogue(d4);
            n4.AppendDialogue(d5);
            n5.AppendDialogue(d6);
            n6.AppendDialogue(d7);

            handler = new DialogueHandler(n1, uiManager, npcName, charVisualization, playerName, playerVisualization);
        }
        else if(character == NPCName.SirBorro)
        {
            DialogueNode n1 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Good morrow! It is I, the Honorable Sir Borro, pleasure to make your acquintance fine sir, or lass. And who might I have the honor of speaking to? >n2
            DialogueNode n2 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Your name, >n3 2) ur mom, >n4 3) Sir Borro the II >n5 4) *spit in his food and leave* *malice +2* >n6 ***starts unavailable until malice is higher, five maybe?***
            DialogueNode n3 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Ah, *your name*, well met! Pray thee, share thy tale. Tell me about yourself. >n7
            DialogueNode n4 = new DialogueNode(new List<Dialogue>(), false); // (Borro) You scurvy knave! You impudent rapscallion! You villanous wretch! Fie, fie to thee and thine kin, and may the Lord above have mercy on thine own mother who had the misfortune of siring you >end ***malice +2****
            DialogueNode n5 = new DialogueNode(new List<Dialogue>(), false); // (Borro) A noble name! Surely you must be a noble warrior, like myself. Prithee, speak thy tale. Who art thou? >n7
            DialogueNode n6 = new DialogueNode(new List<Dialogue>(), false); // (Borro) How darest thou, insolent wretch! Thou art a cur, a scoundrel, a knave through and through! Have at thee! >n12
            DialogueNode n7 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) I am but a humble innkeeper, owner of this fine establishment. I think I might be cursed. >n8 2) Only a humble innkeeper. How are you liking your stay here? How's the food? >n9 3) I'm no one of consequence. What about you, what's your story? >n10 4) *Leave* >end
            DialogueNode n8 = new DialogueNode(new List<Dialogue>(), false); // (Borro) A curse, eh? Fascinating. Who cursed you? Why would anyone do such a thing to such a fine fellow? What kind of curse is it? Whoever it was must hate you. It could be anyone in this very inn, could be someone watching you right here, right now... well, once you expose this evil sorcerer, tell me and we shall vanquish their dark magic forever, together! >n13
            DialogueNode n9 = new DialogueNode(new List<Dialogue>(), false); // (Borro) The victuals look fair enough, yet I still eye them closely, lest poison be hid therein, for mine enemies are ever watchful. >n16
            DialogueNode n10 = new DialogueNode(new List<Dialogue>(), false); // (Borro) I thank thee for thy asking, good sir. I am called Sir Borro, knight-errant, servant of the light and foe to darkness. I roam the world seeking only to serve the good and to honor my lady love, the honorable Octavia the Fifth. >n11
            DialogueNode n11 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) What kinds of evil do you fight, and what brings you to this inn? >n20 2) Who is Lady Octavia the Fifth? >n21 3) leave >end
            DialogueNode n12 = new DialogueNode(new List<Dialogue>(), false); // (Borro) *** Sir Borro throws his food at you. Gain Beef Stew *** >end
            DialogueNode n13 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Thank you, good knight. I thank you for your service, and look forward to fighting that evil together when the hour comes. ** +1 honor ** >n14 2) If it could be anyone in this inn, how do I know it wasn't you?? ** +1 malice **  >n15 3) leave >end
            DialogueNode n14 = new DialogueNode(new List<Dialogue>(), false); // (Borro) As do I, good sir, as do I. No creature of darkness shall escape the judgment of light. Thou art an honorable servant of light. Tell me more of thy tale. >n7
            DialogueNode n15 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Thou darest acuse a knight of allying with the forces of evil? Clearly you know nothing of such matters. I am sworn to defend all that is good and true, and destroy all the tricks and traps of darkness. Nothing shall hinder me in my quest, not even the doubts of one such as you. Now, tell me more of thy tale, knave. >n7
            DialogueNode n16 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) I'm sure the food is fine, who would want to poison you? You should enjoy good food while you have it! >n17 2) If you think there's risk of poison, give me the food and I'll inspect it personally. >n18 3) ***starts unavailable, only available after talking to Gastou about allergies, maybe saveData bool*** Aren't you allergic to meat? **+2 kindness** >n19 4) leave >end
            DialogueNode n17 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Who can say who might seek my poisoning? I have many enemies, and there is something amiss with this inn. But let us forget about such dark matters. Tell me more of thyself, good sir. >n7
            DialogueNode n18 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Mayhaps the morsels are sound, yet I dare not risk any ailments. Better to continue inspecting them, to be safe. Pray thee, let us speak no more of my victuals. Regale me more with thine own tale. >n7
            DialogueNode n19 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Ah-- meat is poison to me! I had near forgot it. I thank thee for the reminder. Take these victuals as payment for thy service. You have my gratitude, good sir! **gain meat stew**  >end
            DialogueNode n20 = new DialogueNode(new List<Dialogue>(), false); // (Borro) ***depends on honor (maybe?) if honor less than three: I'm afraid I must reserve that information for trustworthy ears. You never know where your enemies lie... if honor >= 3: I hunt all manner of beasts born of the darkness. I have vanquished ghosts, ghouls, vampires, demons, and many other malicious spirits from beyond. Of late I have heard tell of a werewolf in these parts, and a sorcerer of great power besides. They have been terrorizing the good folk hereabouts with curses and bloodshed, and I am come to give them what is rightly owed. But hush, keep silent, beware of unwanted ears listening in... trust no one in this inn. No one. >n7
            DialogueNode n21 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Ah, me! My sweet lady! How could anyone live without basking in the radiance of her beauty? How can a soul so lowly as mine begin to describe the depths of her brilliance? Her smile outshines the morning sun yet doth not scorch the eyes, her laugh is as a gentle bell upon a spring morn, and her gaze alone hath undone more foes than ever I have by sword. If ever you have the honor of meeting her, you may consider your life fulfilled, whatever your past transgressions. >n22
            DialogueNode n22 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) She sounds lovely, and I hope to meet her soon! **+2 kindness** >n23 2) Ain't no way she's all that. **+4 malice** >n24 3) leave >end
            DialogueNode n23 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Truly, lovely beyond all belief, and beautiful beyond all comprehension. If ever you do lay eyes on her noble personage, send regards from her noble knight, Sir Borro, who waits on her always. But we could speak of the Lady Octavia for ages and ages and never capture the immensity of her nobility and grace. Let us return to matters closer at hand. Tell me more of thy tale, good sir. >n7
            DialogueNode n24 = new DialogueNode(new List<Dialogue>(), false); // (Borro) Wretch! Varmint! Roach! Foul beast! How darest thou sully the good name of goodness herself! If not for the laws of hospitality, I would cut you down on spot! An inferno of rage roars within me. I will endeavor to forget this moment, and you better pray that I do or it will not end well for you... let us forget her ladyship, pinacle of all that is good. Back to your story, you wretched scum. >n7
            DialogueNode end = new DialogueNode(new List<Dialogue>(), false); // empty node, end node to end dialogue 

            Dialogue dLeave = new Dialogue(end, "*Leave*", true);
            Dialogue d1 = new Dialogue(n2, "Good morrow! It is I, the Honorable Sir Borro, pleasure to make your acquintance fine sir, or lass. And who might I have the honor of speaking to?", true); // stored in n1
            Dialogue d2 = new Dialogue(n3, "I am " + SaveData.saveData.GetName() + ".", true); // stored in n2
            Dialogue d3 = new Dialogue(n4, "ur mom", true); // stored in n2
            Dialogue d4 = new Dialogue(n5, "Sir Borro II.", true); // stored in n2
            Dialogue d5 = new Dialogue(n6, "*Spit in his food and leave*", false, 2, 0, 0, "Do malice/honor/kindness once", "Slightly mean", this); //stored in n2        //needs malice higher than 5
            Dialogue d6 = new Dialogue(n7, "Ah, " + SaveData.saveData.GetName() + ", well met! Pray thee, share thy tale. Tell me about yourself.", true); // stored in n3
            Dialogue d7 = new Dialogue(end, "You scurvy knave! You impudent rapscallion! You villanous wretch! Fie, fie to thee and thine kin, and may the Lord above have mercy on thine own mother who had the misfortune of siring you", true, 2, 0, 0, "Do malice/honor/kindness once", "Default", this); //stored in n4     // malice +2
            Dialogue d8 = new Dialogue(n7, "A noble name! Surely you must be a noble warrior, like myself. Prithee, speak thy tale. Who art thou?", true); // stored in n5
            Dialogue d9 = new Dialogue(n12, "How darest thou, insolent wretch! Thou art a cur, a scoundrel, a knave through and through! Have at thee!", true); // stored in n6
            Dialogue d10 = new Dialogue(n8, "I am but a humble innkeeper, owner of this fine establishment. I think I might be cursed.", true); // stored in n7
            Dialogue d11 = new Dialogue(n9, "How are you liking your stay here? How's the food?", true); // stored in n7
            Dialogue d12 = new Dialogue(n10, "I'm no one of consequence. What about you, what's your story?", true); // stored in n7
            // dLeave in n7
            Dialogue d13 = new Dialogue(n13, "A curse, eh? Fascinating. Who cursed you? Why would anyone do such a thing to such a fine fellow? What kind of curse is it? Whoever it was must hate you. It could be anyone in this very inn, could be someone watching you right here, right now... well, once you expose this evil sorcerer, tell me and we shall vanquish their dark magic forever, together!", true); // stored in n8
            Dialogue d14 = new Dialogue(n16, "The victuals look fair enough, yet I still eye them closely, lest poison be hid therein, for mine enemies are ever watchful.", true); //stored in n9
            Dialogue d15 = new Dialogue(n11, "I thank thee for thy asking, good sir. I am called Sir Borro, knight-errant, servant of the light and foe to darkness. I roam the world seeking only to serve the good and to honor my lady love, the honorable Octavia the Fifth.", true); // stored in n10
            Dialogue d16 = new Dialogue(n20, "What kinds of evil do you fight, and what brings you to this inn?", true); // stored in n11
            Dialogue d17 = new Dialogue(n21, "Who is Lady Octavia the Fifth?", true); // stored in n11
            // dLeave in n11
            Dialogue d18 = new Dialogue(end, "*Sir Borro throws his food at you. Gain Beef Stew*", true, 0, 0, 0, "Add Item", "Default", this); // stored in n12
            Dialogue d19 = new Dialogue(n14, "Thank you, good knight. Maybe we can fight that evil together, when the hours comes.", true, 0, 1, 0, "Do malice/honor/kindness once", "Default", this); // stored in n13
            Dialogue d20 = new Dialogue(n15, "If it could be anyone in this inn, how do I know it wasn't you??", true, 1, 0, 0, "Do malice/honor/kindness once", "Default", this); //stored in n13
            // dLeave in n13
            Dialogue d21 = new Dialogue(n7, "As do I, good sir, as do I. No creature of darkness shall escape the judgment of light. Thou art an honorable servant of light. Tell me more of thy tale.", true); //stored in n14
            Dialogue d22 = new Dialogue(n7, "Thou darest acuse a knight of allying with the forces of evil? Clearly you know nothing of such matters. I am sworn to defend all that is good and true, and destroy all the tricks and traps of darkness. Nothing shall hinder me in my quest, not even the doubts of one such as you. Now, tell me more of thy tale, knave.", true); // stored in n15
            Dialogue d23 = new Dialogue(n17, "I'm sure the food is fine, who would want to poison you?", true); // stored in n16
            Dialogue d24 = new Dialogue(n18, "If you think there's risk of poison, give me the food and I'll inspect it personally.", true); // stored in n16
            Dialogue d25 = new Dialogue(n19, "Aren't you allergic to meat?", false, 0, 0, 2, "Do malice/honor/kindness once", "Check knows allergy", this); // stored in n16
            // dLeave in n16
            Dialogue d26 = new Dialogue(n7, "Who can say who might seek my poisoning? I have many enemies, and there is something amiss with this inn. But let us forget about such dark matters. Tell me more of thyself, good sir.", true); // stored in n17
            Dialogue d27 = new Dialogue(n7, "Mayhaps the morsels are sound, yet I dare not risk any ailments. Better to continue inspecting them, to be safe. Pray thee, let us speak no more of my victuals. Regale me more with thine own tale.", true); // stored in n18
            Dialogue d28 = new Dialogue(end, "Ah-- meat is poison to me! I had near forgot it. I thank thee for the reminder. Take these victuals as payment for thy service. You have my gratitude, good sir! \n**gain meat stew**", true, 0, 0, 0, "Add Item", "Default", this); // stored in n19
            Dialogue d29 = new Dialogue(n7, "I'm afraid I must reserve that information for trustworthy ears. You never know where your enemies lie... ", true, 0, 0, 0, "Update message based on honor (Sir Borro)", "Default", this); // stored in n20
            Dialogue d30 = new Dialogue(n22, "Ah, me! My sweet lady! How could anyone live without basking in the radiance of her beauty? How can a soul so lowly as mine begin to describe the depths of her brilliance? Her smile outshines the morning sun yet doth not scorch the eyes, her laugh is as a gentle bell upon a spring morn, and her gaze alone hath undone more foes than ever I have by sword. If ever you have the honor of meeting her, you may consider your life fulfilled, whatever your past transgressions.", true); // stored in n21
            Dialogue d31 = new Dialogue(n23, "She sounds lovely, and I hope to meet her soon!", true, 0, 0, 2, "Do malice/honor/kindness once", "Default", this); // stored in n22
            Dialogue d32 = new Dialogue(n24, "Ain't no way she's all that.", true, 4, 0, 0, "Do malice/honor/kindness once", "Default", this); // stored in n22
            // dLeave in n22
            Dialogue d33 = new Dialogue(n7, "Truly, lovely beyond all belief, and beautiful beyond all comprehension. If ever you do lay eyes on her noble personage, send regards from her noble knight, Sir Borro, who waits on her always. But we could speak of the Lady Octavia for ages and ages and never capture the immensity of her nobility and grace. Let us return to matters closer at hand. Tell me more of thy tale, good sir.", true); // in n23
            Dialogue d34 = new Dialogue(n7, "Wretch! Varmint! Roach! Foul beast! How darest thou sully the good name of goodness herself! If not for the laws of hospitality, I would cut you down on spot! An inferno of rage roars within me. I will endeavor to forget this moment, and you better pray that I do or it will not end well for you... let us forget her ladyship, pinacle of all that is good. Back to your story, you wretched scum.", true); // in n24

            n1.AppendDialogue(d1);
            n2.AppendDialogue(d2);
            n2.AppendDialogue(d3);
            n2.AppendDialogue(d4);
            n2.AppendDialogue(d5);
            n3.AppendDialogue(d6);
            n4.AppendDialogue(d7);
            n5.AppendDialogue(d8);
            n6.AppendDialogue(d9);
            n7.AppendDialogue(d10);
            n7.AppendDialogue(d11);
            n7.AppendDialogue(d12);
            n7.AppendDialogue(dLeave);
            n8.AppendDialogue(d13);
            n9.AppendDialogue(d14);
            n10.AppendDialogue(d15);
            n11.AppendDialogue(d16);
            n11.AppendDialogue(d17);
            n11.AppendDialogue(dLeave);
            n12.AppendDialogue(d18);
            n13.AppendDialogue(d19);
            n13.AppendDialogue(d20);
            n13.AppendDialogue(dLeave);
            n14.AppendDialogue(d21);
            n15.AppendDialogue(d22);
            n16.AppendDialogue(d23);
            n16.AppendDialogue(d24);
            n16.AppendDialogue(d25);
            n16.AppendDialogue(dLeave);
            n17.AppendDialogue(d26);
            n18.AppendDialogue(d27);
            n19.AppendDialogue(d28);
            n20.AppendDialogue(d29);
            n21.AppendDialogue(d30);
            n22.AppendDialogue(d31);
            n22.AppendDialogue(d32);
            n22.AppendDialogue(dLeave);
            n23.AppendDialogue(d33);
            n24.AppendDialogue(d34);

            handler = new DialogueHandler(n1, uiManager, npcName, charVisualization, playerName, playerVisualization);
        }
        else if(character == NPCName.Gastou)
        {
            DialogueNode n1 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Ah! Welcome, welcome mon ami! Sit with me, 'ave a drink, we chat. >n2
            DialogueNode n2 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Who are you, what's your story? >n3 2) What brings you to this inn? >n4 3) How do you like the food? >n5 4) *leave*
            DialogueNode n3 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) I come from ze mountains. Cold air, sharp paths, forests zat run forever. I used to run through ze trees like an animal myself—no roads, no walls, only breath and bark and snow. At night, you hear ze wolves. Not close… but close enough zat you listen instead of sleep. Zey howl, and ze mountains answer. It is… honest sound. >n6
            DialogueNode n4 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Zis inn is warm, and ze night winds are cold. But I will not stay for long. Zis inn... it watches you. You feel it, yes? >n10
            DialogueNode n5 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Zis meal? It is solid. It stays where you put it, and it does not argue. Zat is good food. >n18
            DialogueNode n6 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Cool. >n7 2) You're crazy >n8 *+2 malice* 3) That's awesome, I wish I had that sort of freedom. >n9 *+2 kindness* 4) *leave* >end
            DialogueNode n7 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Yes. Ze wind was cool, cool to ze bone. >n2
            DialogueNode n8 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Crazy? I was crazy once. You are rude. It is no matter. You are also small and weak and fat. And bald. Why no hair grow on your head? Where did it all go? >n2
            DialogueNode n9 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Freedom. Yes, zat is a good word for it. Ze feel of ze wind on your face as you run and leap from peak to peak, one with nature and natural with oneself. It is good, I think. But zis inn is nice too, good rest, good food. >n2
            DialogueNode n10 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Yes, I feel it. Like someone is always watching you. >n11 2) Hey this is my inn, don't talk bad about it! >n12 3) I feel it, and I promise to you I will make whatever is wrong right so travelers like you may rest easier *+2 honor* >n13 4) *leave* >end
            DialogueNode n11 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Exactly. Too many eyes, too many pauses in conversation. People smile, but zey do not relax. Something is wrong ’ere. I do not know what—but I would not turn my back on zis room. >n14
            DialogueNode n12 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Bah… I speak too sharp sometimes. Ze mountains teach you to be honest, not polite. Yet it is true, yes? Something is wrong here... >n2
            DialogueNode n13 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) A place of true rest. That would be nice. Bonne chance, mon ami. Good luck. >n2
            DialogueNode n14 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) What do you know about the knight sitting over there? >n15 2) What can you tell me about the butcher behind the bar? >n16 3) I hear noise from that door. Are there more people here than we see? >n17 4) leave >end
            DialogueNode n15 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Ze knight? ’E has fancy armor, yes—but not much underneath. I spoke to ’im when I came in and ’e went on and on about some lady. All sighs and poetry. Not quite right in ze head, oui? So I tell ’im, eat up! ’Ave some meat, put a little flesh on ze bone. And zen—listen to zis—’e tells me ’e is allergic. Allergic… to meat. How does one even live like zat? >n14 **savedata SetAllergic bool
            DialogueNode n16 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Ze Butcher? 'Is knife is sharp, and 'is hands are quick. Maybe quicker zen 'is wits. >n14
            DialogueNode n17 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Ah, ze door. Oui, I think zer are more people, but ze door is locked. Ze butcher 'as ze key, I think. >n2
            DialogueNode n18 = new DialogueNode(new List<Dialogue>(), true); // (Player) 1) I'm glad you like the food. And in fact, just for you, take it on the house. **+2 kindness** >n19 2) It's pretty good isn't it. Can I have it? Please? >n20 3) *Take his food and leave* **+3 malice** >n21 4) *leave* >end
            DialogueNode n19 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Bah. I need no charity. But still, zat was generous. You 'ave my thanks. >n2
            DialogueNode n20 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) Non. Zis is mine. Get your own. >n2
            DialogueNode n21 = new DialogueNode(new List<Dialogue>(), false); // (Gastou) *He holds his food tightly. He is much stronger than you, and as you walk away with no food, Gastou glares at you menacingly and grunts.* >end
            DialogueNode end = new DialogueNode(new List<Dialogue>(), false); // empty node, end node to end dialogue 

            Dialogue dLeave = new Dialogue(end, "*Leave*", true);
            Dialogue d1 = new Dialogue(n2, "Ah! Welcome, welcome mon ami! Sit with me, 'ave a drink, we chat.", true); // stored in n1
            Dialogue d2 = new Dialogue(n3, "Who are you, what's your story?", true); // stored in n2
            Dialogue d3 = new Dialogue(n4,"What brings you to this inn?", true); // stored in n2
            Dialogue d4 = new Dialogue(n5, "How do you like the food?", true); // stored in n2
            // dLeave in n2
            Dialogue d5 = new Dialogue(n6, "I come from ze mountains. Cold air, sharp paths, forests zat run forever. I used to run through ze trees like an animal myself—no roads, no walls, only breath and bark and snow. At night, you hear ze wolves. Not close… but close enough zat you listen instead of sleep. Zey howl, and ze mountains answer. It is… honest sound.", true); // stored in n3
            Dialogue d6 = new Dialogue(n10, "Zis inn is warm, and ze night winds are cold. But I will not stay for long. Zis inn... it watches you. You feel it, yes?", true); // stored in n4
            Dialogue d7 = new Dialogue(n18, "Zis meal? It is solid. It stays where you put it, and it does not argue. Zat is good food.", true); //stored in n5
            Dialogue d8 = new Dialogue(n7, "Cool.", true); // stored in n6
            Dialogue d9 = new Dialogue(n8, "You're crazy.", true, 2, 0, 0, "Do malice/honor/kindness once", "Default", this); // stored in n6
            Dialogue d10 = new Dialogue(n9, "That's awesome, I wish I had that sort of freedom.", true, 0, 0, 2, "Do malice/honor/kindness once", "Default", this); // stored in n6
            //dLeave in n6
            Dialogue d11 = new Dialogue(n2, "Yes. Ze wind was cool, cool to ze bone.", true); // stored in n7
            Dialogue d12 = new Dialogue(n2, "Crazy? I was crazy once. You are rude. It is no matter. You are also small and weak and fat. And bald. Why no hair grow on your head? Where did it all go?", true); // stored in n8
            Dialogue d13 = new Dialogue(n2, "Freedom. Yes, zat is a good word for it. Ze feel of ze wind on your face as you run and leap from peak to peak, one with nature and natural with oneself. It is good, I think. But zis inn is nice too, good rest, good food.", true); // stored in n9
            Dialogue d14 = new Dialogue(n11, "Yes, I feel it. Like someone is always watching you.", true); // stored in n10
            Dialogue d15 = new Dialogue(n12, "Hey this is my inn, don't talk bad about it!", true); // stored in n10
            Dialogue d16 = new Dialogue(n13, "I feel it, and I promise to you I will make whatever is wrong right so travelers like you may rest easier", true, 0, 2, 0, "Do malice/honor/kindness once", "Default", this); // stored in n10
            //dleave in n10
            Dialogue d17 = new Dialogue(n14, "Exactly. Too many eyes, too many pauses in conversation. People smile, but zey do not relax. Something is wrong ’ere. I do not know what—but I would not turn my back on zis room.", true); // stored in n11
            Dialogue d18 = new Dialogue(n2, "Bah… I speak too sharp sometimes. Ze mountains teach you to be honest, not polite. Yet it is true, yes? Something is wrong here...", true); // stored in n12
            Dialogue d19 = new Dialogue(n2, "A place of true rest. That would be nice. Bonne chance, mon ami. Good luck.", true); // stored in n13
            Dialogue d20 = new Dialogue(n15, "What do you know about the knight sitting over there?", true); // stored in n14
            Dialogue d21 = new Dialogue(n16, "What can you tell me about the butcher behind the bar? ", true); // stored in n14
            Dialogue d22 = new Dialogue(n17, "I hear noise from that door. Are there more people here than we see?", true); // stored in n14
            //dLeave in n14
            Dialogue d23 = new Dialogue(n14, "Ze knight? 'E has fancy armor, yes—but not much underneath. I spoke to 'im when I came in and 'e went on and on about some lady. All sighs and poetry. Not quite right in ze head, oui? So I tell ’im, eat up! ’Ave some meat, put a little flesh on ze bone. And zen—listen to zis—’e tells me ’e is allergic. Allergic… to meat. How does one even live like zat?", true, 0, 0, 0, "Update allergy knowledge", "Default", this); // stored in n15, **updates allergy knowledge
            Dialogue d24 = new Dialogue(n14, "Ze Butcher? 'Is knife is sharp, and 'is hands are quick. Maybe quicker zen 'is wits.", true); // stored in n16
            Dialogue d25 = new Dialogue(n2, "Ah, ze door. Oui, I think zer are more people, but ze door is locked. Ze butcher 'as ze key, I think.", true); // stored in n17
            Dialogue d26 = new Dialogue(n19, "I'm glad you like the food. And in fact, just for you, take it on the house.", true, 0, 0, 2, "Do malice/honor/kindness once", "Default", this); // stored in n18
            Dialogue d27 = new Dialogue(n20, "It's pretty good isn't it. Can I have it? Please?", true); // stored in n18
            Dialogue d28 = new Dialogue(n21, "*Take his food and leave*", true, 3, 0, 0, "Do malice/honor/kindness once", "Default", this); // stored in n18
            //dLeave in n18
            Dialogue d29 = new Dialogue(n2, "Bah. I need no charity. But still, zat was generous. You 'ave my thanks.", true); // stored in n19
            Dialogue d30 = new Dialogue(n2, "Non. Zis is mine. Get your own.", true); // stored in n20
            Dialogue d31 = new Dialogue(end, "*He holds his food tightly. He is much stronger than you, and as you walk away with no food, Gastou glares at you menacingly and grunts.*", true); // stored in n21

            n1.AppendDialogue(d1);
            n2.AppendDialogue(d2);
            n2.AppendDialogue(d3);
            n2.AppendDialogue(d4);
            n2.AppendDialogue(dLeave);
            n3.AppendDialogue(d5);
            n4.AppendDialogue(d6);
            n5.AppendDialogue(d7);
            n6.AppendDialogue(d8);
            n6.AppendDialogue(d9);
            n6.AppendDialogue(d10);
            n6.AppendDialogue(dLeave);
            n7.AppendDialogue(d11);
            n8.AppendDialogue(d12);
            n9.AppendDialogue(d13);
            n10.AppendDialogue(d14);
            n10.AppendDialogue(d15);
            n10.AppendDialogue(d16);
            n10.AppendDialogue(dLeave);
            n11.AppendDialogue(d17);
            n12.AppendDialogue(d18);
            n13.AppendDialogue(d19);
            n14.AppendDialogue(d20);
            n14.AppendDialogue(d21);
            n14.AppendDialogue(d22);
            n14.AppendDialogue(dLeave);
            n15.AppendDialogue(d23);
            n16.AppendDialogue(d24);
            n17.AppendDialogue(d25);
            n18.AppendDialogue(d26);
            n18.AppendDialogue(d27);
            n18.AppendDialogue(d28);
            n18.AppendDialogue(dLeave);
            n19.AppendDialogue(d29);
            n20.AppendDialogue(d30);
            n21.AppendDialogue(d31);

            handler = new DialogueHandler(n1, uiManager, npcName, charVisualization, playerName, playerVisualization);


        }
        else if (character == NPCName.PohnJork)
        {
            DialogueNode n1 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) 'Sup. >n2
            DialogueNode n2 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Yo. >n3 2) Hello there! Who are you, what's your story? >n4 3) Give me the key. >n5 4) *leave* >end
            DialogueNode n3 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Chilling. Cya 'round > end
            DialogueNode n4 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) I'm just some dude who works here. I'm a butcher, I chop a lot with meat. Actually I need more meat. Do you have any meat? >n6
            DialogueNode n5 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) What key? I don't have any key. None for you, certainly, at least not for free... >n29
            DialogueNode n6 = new DialogueNode(new List<Dialogue>(), true); // (Player) 1) Why would I give you my meat? >n7 2) Not at the moment, but I swear to you I shall search out some meat and bring it back to you with all haste *+2 honor* >n8 3) **starts unavailable** yeah I have your meat right here >n9 4) *leave* >end
            DialogueNode n7 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Pause. Nah I'm playing. I don't know, it would be a nice thing to do. I also have a key to the other room you might want, though I locked the door for a reason. There are some certified crazies over there, and that coming from me. >n10
            DialogueNode n8 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Whoa, bro, chill out. No need to be so dramatic. But I'll take you up on that offer, and maybe even give you a little something in return. >n2
            
            // Starter node when holding meat in your hand
            DialogueNode n9 = new DialogueNode(new List<Dialogue>(), false); //(Pohn) You have meat for me? >n11

            DialogueNode n10 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) What kind of crazies? >n18 2) Oh don't say that, you're not crazy! *+2 kindness* >n19 3) leave >end
            DialogueNode n11 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Give Meat Stew **get rid of meat stew item** >n12 2) I have meat. Not for you. You want to smell it? It smells so good, so fresh and scrumptious. Mmmm. **+2 malice** >n13 3) leave >end
            DialogueNode n12 = new DialogueNode(new List<Dialogue>(), false); //(Pohn) Yooo, thanks my man, I've been needing this meat for so long. Now I can cut it up over and over into small pieces with my cleaver. It's fun, the way it chops through flesh. Like cutting butter, only it's meat. Going chop chop chop! Chop chop chop! >n14
            DialogueNode n13 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Whatever bro. >n2
            DialogueNode n14 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) ... >n15
            DialogueNode n15 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Hey, maybe I could chop up a pork chop for you! Chop chop chop! >n16
            DialogueNode n16 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) ... >n17
            DialogueNode n17 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Oh right, here's the key to the over room. Thanks for the meat! This'll keep me occupied for a while. Chop chop chop, chop chop chop... *gain simple key* >end
            DialogueNode n18 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Well, there's our Mayor, our shopkeeper, our barber, and our crazy old man and woman. Who do you want to hear about? >n20
            DialogueNode n19 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Uh huh. Well that's nice of you to say. And thank you to the little piggies flying over your shoulder, they look nice too. I bet they'd be fun to chop up and roast over an open fire... >n2
            DialogueNode n20 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Tell me about the Mayor. >n21 2) Tell me about the shopkeeper. >n22 3) Tell me about the barber. >n23 4) Tell me about the Elderly couple >n24
            DialogueNode n21 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Mayor Mary Thadworn is the poorest person in this inn. She's always begging for scraps from my meat, as if that stuff just grows on trees. She's probably out begging, right now. We voted her mayor out of pity in hopes she won't be so annoying if she's busy, but it only inflated her ego even more. I think she's close with Lupal. That's about all I know. >n10
            DialogueNode n22 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Octavia? She's an arse. Led me on for a whole month saying she'd sell me some meat, not just any meat but a prime cut of pork rib. Do you know how valuable that is? How satisfying it would be to cleave in half with this here knife? But also she's a scumbag to everyone in the village, pretends to be nice then stabs them in the back. I think that knight guy is falling into her trap, not that I'm gonna warn him. Might be fun to watch. >n10
            DialogueNode n23 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Harry the barber is as bald as an egg and super self conscious about it. He's a great hairdresser, don't get me wrong, but I think all these years of touching others' hair without ever tending to his own may have rubbed off on him. And never, under any circumstances, tell him he's bald to his face. It will not end well. >n10
            DialogueNode n24 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Ah, good old Lupal and Wyl. They're married, not that they'll ever let you know it. They're the main reason I locked the door to be honest. Wyl's our blacksmith, and a solid one at that, but all that clanging and hot fires have gotten to his head. Sometimes he just spins in a circle, giggling to himself. Gives me the creeps. Then Lupal's even worse. She's always creeping around the forest at night, picking out herbs and other substances for her concoctions. >n25
            DialogueNode n25 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) Why do they fight? >n26 2) Does Wyl still smith if he's insane? >n27 3) What kind of concoctions does Lupal make? >n28 4) leave >end
            DialogueNode n26 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Who knows. Some say Wyl saw Lupal doing some black magic and she cursed him to forget it with one of her potions. Others say Wyl cares more for his swords and axes then his wife. Personally I think they're both so old they've forgotten they're married and just ignore each other, but no one really knows. >n10
            DialogueNode n27 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Oh yeah. I'm surprised he's here now, actually. He's almost always holed up in his forge, banging away at his newest project. He probably has some weapons on sale now, for the right price. >n10
            DialogueNode n28 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) I don't know and I don't want to find out. She mixes things together in these little bottles Wyl makes for her, and if you drink it strange things will happen. One time I swear I saw smoke coming out of Wyl's ears after he drank one of them, and I know for a fact that Harry tried one and could only speak in French for a whole month. It was terrible, for everyone except Gastou, who loved it. >n10
            DialogueNode n29 = new DialogueNode(new List<Dialogue>(), true); // (Player) Options: 1) And what would that price be? >n30 2) *push him over and take the key* >n31 3) leave >end
            DialogueNode n30 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) Meat. Red, juicy, delicious meat. Meat in a bowl, meat off the bone, meat fresh, meat old, I don't care I just want meat. You have any? >n6
            DialogueNode n31 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) *You push Pohn Jork. Pohn Jork is falling. Before you can take the key, he jumps to his feet and grabs his cleaver, which he brandishes menacingly* **+3 malice** >n32
            DialogueNode n32 = new DialogueNode(new List<Dialogue>(), false); // (Pohn) That was not very nice of you. Not very nice at all. You know, you have meat on your bones. Meat that would be quite satisfying to cleave, to cut, to chop, chop, chop into little pieces. So satisfying... but no, last time they got very angry at Pohn for chopping that kind of meat. Promised never to do it again. Not gonna break that promise over some fat bald guy. Pohn Jork is fine. Pohn Jork is chilling. >n2
            DialogueNode end = new DialogueNode(new List<Dialogue>(), false); // empty node

            Dialogue dLeave = new Dialogue(end, "*Leave*", true);

            Dialogue d1 = new Dialogue(n2, "'Sup.", true); // stored in n1
            Dialogue d2 = new Dialogue(n3, "Yo.", true); // stored in n2
            Dialogue d3 = new Dialogue(n4, "Hello there! Who are you, what's your story?", true); // stored in n2
            Dialogue d4 = new Dialogue(n5, "Give me the key.", true); // stored in n2
            //dleave in n2
            Dialogue d5 = new Dialogue(end, "Chilling. Cya 'round", true); // stored in n3
            Dialogue d6 = new Dialogue(n6, "I'm just some dude who works here. I'm a butcher, I chop a lot with meat. Actually I need more meat. Do you have any meat?", true); // stored in n4
            Dialogue d7 = new Dialogue(n29, "What key? I don't have any key. None for you, certainly, at least not for free...", true); // stored in n5
            Dialogue d8 = new Dialogue(n7, "Why would I give you my meat?", true); // stored in n6
            Dialogue d9 = new Dialogue(n8, "Not at the moment, but I swear to you I shall search out some meat and bring it back to you with all haste.", true, 0, 2, 0, "Do malice/honor/kindness once", "Default", this); // stored in n6
            Dialogue d10 = new Dialogue(n9, "Yeah I have your meat right here", false, 0, 0, 0, "Do nothing", "Check has meat", this); // stored in n6
            //dLeave in n6
            Dialogue d11 = new Dialogue(n10, "Pause. Nah I'm playing. I don't know, it would be a nice thing to do. I also have a key to the other room you might want, though I locked the door for a reason. There are some certified crazies over there, and that coming from me.", true); // stored in n7
            Dialogue d12 = new Dialogue(n2, "Whoa, bro, chill out. No need to be so dramatic. But I'll take you up on that offer, and maybe even give you a little something in return.", true); // stored in n8
            Dialogue d13 = new Dialogue(n11, "You have meat for me?", true); // stored in n9
            Dialogue d14 = new Dialogue(n18, "What kind of crazies?", true); // stored in n10
            Dialogue d15 = new Dialogue(n19, "Oh don't say that, you're not crazy!", true); // stored in n10
            //dLeave in n10
            Dialogue d16 = new Dialogue(n12, "*Give Beef Stew*", true); // stored in n11                // THIS SHOULD GO TO A FUNCTION TO ACTUALLY GET RID OF THE STEW... NO TIME THO FOR NOW
            Dialogue d17 = new Dialogue(n13, "I have meat. Not for you. You want to smell it? It smells so good, so fresh and scrumptious. Mmmm.", true, 2, 0, 0, "Do malice/honor/kindness once", "Default", this); // stored in n11
            //dLeave in n11
            Dialogue d18 = new Dialogue(n14, "Yooo, thanks my man, I've been needing this meat for so long. Now I can cut it up over and over into small pieces with my cleaver. It's fun, the way it chops through flesh. Like cutting butter, only it's meat. Going chop chop chop! Chop chop chop!", true); // stored in n12
            Dialogue d19 = new Dialogue(n2, "Whatever bro.", true); // stored in n13
            Dialogue d20 = new Dialogue(n15, "...", true); // stored in n14
            Dialogue d21 = new Dialogue(n16, "Hey, maybe I could chop up a pork chop for you! Chop chop chop!", true); // stored in n15
            Dialogue d22 = new Dialogue(n17, "...", true); // stored in n16
            Dialogue d23 = new Dialogue(end, "Oh right, here's the key to the over room. Thanks for the meat! This'll keep me occupied for a while. Chop chop chop, chop chop chop... \n*gain simple key*", true, 0, 0, 0, "Add Item", "Default", this); // stored in n17
            Dialogue d24 = new Dialogue(n20, "Well, there's our Mayor, our shopkeeper, our barber, and our crazy old man and woman. Who do you want to hear about?", true); // stored in n18
            Dialogue d25 = new Dialogue(n2, "Uh huh. Well that's nice of you to say. And thank you to the little piggies flying over your shoulder, they look nice too. I bet they'd be fun to chop up and roast over an open fire...", true); // stored in n19
            Dialogue d26 = new Dialogue(n21, "Tell me about the Mayor.", true); // stored in n20
            Dialogue d27 = new Dialogue(n22, "Tell me about the shopkeeper.", true); // stored in n20
            Dialogue d28 = new Dialogue(n23, "Tell me about the barber.", true); // stored in n20
            Dialogue d29 = new Dialogue(n24, "Tell me about the Elderly couple", true); // stored in n20
            Dialogue d30 = new Dialogue(n10, "Mayor Mary Thadworn is the poorest person in this inn. She's always begging for scraps from my meat, as if that stuff just grows on trees. She's probably out begging, right now. We voted her mayor out of pity in hopes she won't be so annoying if she's busy, but it only inflated her ego even more. I think she's close with Lupal. That's about all I know.", true); // stored in n21
            Dialogue d31 = new Dialogue(n10, "Octavia? She's an arse. Led me on for a whole month saying she'd sell me some meat, not just any meat but a prime cut of pork rib. Do you know how valuable that is? How satisfying it would be to cleave in half with this here knife? But also she's a scumbag to everyone in the village, pretends to be nice then stabs them in the back. I think that knight guy is falling into her trap, not that I'm gonna warn him. Might be fun to watch.", true); // stored in n22
            Dialogue d32 = new Dialogue(n10, "Harry the barber is as bald as an egg and super self conscious about it. He's a great hairdresser, don't get me wrong, but I think all these years of touching others' hair without ever tending to his own may have rubbed off on him. And never, under any circumstances, tell him he's bald to his face. It will not end well.", true); // stored in n23
            Dialogue d33 = new Dialogue(n25, "Ah, good old Lupal and Wyl. They're married, not that they'll ever let you know it. They're the main reason I locked the door to be honest. Wyl's our blacksmith, and a solid one at that, but all that clanging and hot fires have gotten to his head. Sometimes he just spins in a circle, giggling to himself. Gives me the creeps. Then Lupal's even worse. She's always creeping around the forest at night, picking out herbs and other substances for her concoctions.", true); // stored in n24
            Dialogue d34 = new Dialogue(n26, "Why do they fight?", true); // stored in n25
            Dialogue d35 = new Dialogue(n27, "Does Wyl still smith if he's insane?", true); // stored in n25
            Dialogue d36 = new Dialogue(n28, "What kind of concoctions does Lupal make?", true); // stored in n25
            //dLeave in n25
            Dialogue d37 = new Dialogue(n10, "Who knows. Some say Wyl saw Lupal doing some black magic and she cursed him to forget it with one of her potions. Others say Wyl cares more for his swords and axes then his wife. Personally I think they're both so old they've forgotten they're married and just ignore each other, but no one really knows.", true); // stored in n26
            Dialogue d38 = new Dialogue(n10, "Oh yeah. I'm surprised he's here now, actually. He's almost always holed up in his forge, banging away at his newest project. He probably has some weapons on sale now, for the right price.", true); // stored in n27
            Dialogue d39 = new Dialogue(n10, "I don't know and I don't want to find out. She mixes things together in these little bottles Wyl makes for her, and if you drink it strange things will happen. One time I swear I saw smoke coming out of Wyl's ears after he drank one of them, and I know for a fact that Harry tried one and could only speak in French for a whole month. It was terrible, for everyone except Gastou, who loved it.", true); // stored in n28
            Dialogue d40 = new Dialogue(n30, "And what would that price be?", true); // stored in n29
            Dialogue d41 = new Dialogue(n31, "*push him over and take the key*", true); // stored in n29
            //dLeave in n29
            Dialogue d42 = new Dialogue(n6, "Meat. Red, juicy, delicious meat. Meat in a bowl, meat off the bone, meat fresh, meat old, I don't care I just want meat. You have any?", true); // stored in n30
            Dialogue d43 = new Dialogue(n32, "*You push Pohn Jork. Pohn Jork is falling. Before you can take the key, he jumps to his feet and grabs his cleaver, which he brandishes menacingly*", true, 3, 0, 0, "Do malice/honor/kindness once", "Default", this); // stored in n31
            Dialogue d44 = new Dialogue(n2, "That was not very nice of you. Not very nice at all. You know, you have meat on your bones. Meat that would be quite satisfying to cleave, to cut, to chop, chop, chop into little pieces. So satisfying... but no, last time they got very angry at Pohn for chopping that kind of meat. Promised never to do it again. Not gonna break that promise over some fat bald guy. Pohn Jork is fine. Pohn Jork is chilling.", true); // stored in n32

            n1.AppendDialogue(d1);
            n2.AppendDialogue(d2);
            n2.AppendDialogue(d3);
            n2.AppendDialogue(d4);
            n2.AppendDialogue(dLeave);
            n3.AppendDialogue(d5);
            n4.AppendDialogue(d6);
            n5.AppendDialogue(d7);
            n6.AppendDialogue(d8);
            n6.AppendDialogue(d9);
            n6.AppendDialogue(d10);
            n6.AppendDialogue(dLeave);
            n7.AppendDialogue(d11);
            n8.AppendDialogue(d12);
            n9.AppendDialogue(d13);
            n10.AppendDialogue(d14);
            n10.AppendDialogue(d15);
            n10.AppendDialogue(dLeave);
            n11.AppendDialogue(d16);
            n11.AppendDialogue(d17);
            n11.AppendDialogue(dLeave);
            n12.AppendDialogue(d18);
            n13.AppendDialogue(d19);
            n14.AppendDialogue(d20);
            n15.AppendDialogue(d21);
            n16.AppendDialogue(d22);
            n17.AppendDialogue(d23);
            n18.AppendDialogue(d24);
            n19.AppendDialogue(d25);
            n20.AppendDialogue(d26);
            n20.AppendDialogue(d27);
            n20.AppendDialogue(d28);
            n20.AppendDialogue(d29);
            n21.AppendDialogue(d30);
            n22.AppendDialogue(d31);
            n23.AppendDialogue(d32);
            n24.AppendDialogue(d33);
            n25.AppendDialogue(d34);
            n25.AppendDialogue(d35);
            n25.AppendDialogue(d36);
            n25.AppendDialogue(dLeave);
            n26.AppendDialogue(d37);
            n27.AppendDialogue(d38);
            n28.AppendDialogue(d39);
            n29.AppendDialogue(d40);
            n29.AppendDialogue(d41);
            n29.AppendDialogue(dLeave);
            n30.AppendDialogue(d42);
            n31.AppendDialogue(d43);
            n32.AppendDialogue(d44);


            handler = new DialogueHandler(n1, uiManager, npcName, charVisualization, playerName, playerVisualization);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasAnimation)
        {
            if(player.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                animator.SetBool("PlayerToLeft", true);
                animator.SetBool("PlayerToRight", false);
            }
            else
            {
                animator.SetBool("PlayerToLeft", false);
                animator.SetBool("PlayerToRight", true);
            }
        }
    }
    
    public void DisplayDialogue()
    {
        handler.DisplayOptions();
    }
    
    public void DoNextDialogue(int option)
    {
        //option must be in range of Dialogue options, or else returns and turns off dialouge and resets currNode
        handler.GoToNextNode(option);
    }

    public bool GetAssociatedItemGiven()
    {
        return associatedItemGiven;
    }

    public void SetAssociatedItemGiven(bool isGiven)
    {
        associatedItemGiven = isGiven;
    }

    // Dialogue Option class to contain dialogue options

}


//     [System.Serializable]
//     public class Dialogue
//     {
//         public Dialogue nextDialogue;
//         public bool isPlayerDialogue;
//         public bool isLast; // true if this is the last dialogue before sending them on their way
//         public string messageName;
//         public string message;
//         public bool originalIsAvailableMessage;
//         public bool isAvailableMessage;

//         // PLAYER STATS ONLY 

//         // Potential consequences for selecting this dialogue option (by default 0)
//         public int selectedMalice;
//         public int selectedHonor;
//         public int selectedKindness;
//         //add more specific quests, like selectedNecromancer or something, as needed

//         //Default constructor: for NPC dialogue only, not player dialogue!
// //         public Dialogue(Dialogue next, string mName, string mMessage, bool startsAvailable, bool last)
//         {
//             nextDialogue = next;
//             isLast = last;
//             isPlayerDialogue = false;
//             messageName = mName;
//             message = mMessage;
//             originalIsAvailableMessage = startsAvailable;
//             isAvailableMessage = startsAvailable;
//             selectedMalice = 0;
//             selectedHonor = 0;
//             selectedKindness = 0;
//         }

//         //Player dialogue option constructor only!
//         public Dialogue(Dialogue next, string mName, string mMessage, bool startsAvailable, int malice, int honor, int kindness, bool last)
//         {
//             nextDialogue = next;
//             isLast = last;
//             isPlayerDialogue = false;
//             messageName = mName;
//             message = mMessage;
//             originalIsAvailableMessage = startsAvailable;
//             isAvailableMessage = startsAvailable;
//             selectedMalice = malice;
//             selectedHonor = honor;
//             selectedKindness = kindness;
//         }

//         public void ToggleAvailable(bool isAvailable)
//         {
//             isAvailableMessage = isAvailable;
//         }

//     public string GetMessage()
//     {
//         return message;
//     }

//     public bool IsLast()
//     {
//         return isLast;
//     }

//     public Dialogue GetNext()
//     {
//         return nextDialogue;
//     }
        
//     }

//     // gonna use bools instead of subclasses ig because SerializeReference isn't working even after 2 hours of GPTing, oh well

//     // [System.Serializable]
//     // public class PlayerDialogue : Dialogue
//     // {
//     //     public int test;
//     // }

//     // [System.Serializable]
//     // public class NPCDialogue : Dialogue
//     // {
//     //     public int testing;
//     // }


//     // Dialogue List
//     [System.Serializable]
//     public class DialogueList
//     {
//         public string listName; // name of this set of dialogue options, e.g. 'base dialogue' or 'angry dialogue' etc
//         public List<Dialogue> dialogueOptions = new List<Dialogue>();
//         public bool isAvailableList;

//         public void ToggleAvailability(bool isAvailable)
//         {
//             foreach(Dialogue dialogue in dialogueOptions)
//             {
//                 dialogue.ToggleAvailable(isAvailable);
//             }
//             isAvailableList = isAvailable;
//         }

//     public Dialogue GetFirstDialogue()
//     {
//         return dialogueOptions[0];
//     }
        
//     }
// [System.Serializable]
// public class DialogueHandler
// {
//     public Dialogue currDialogue;
//     public List<DialogueList> allDialogueLists;
//     public DialogueList currDialogueList;
//     public UIManager uiManager;
//     public Sprite charVis;
//     public string charName;

//     public DialogueHandler(Dialogue curr, List<DialogueList> currList, UIManager ui, Sprite npc, string charName)
//     {
//         currDialogue = curr;
//         allDialogueLists = currList;
//         currDialogueList = currList[0];
//         uiManager = ui;
//         charVis = npc;
//     }

//     public void NextDialogueOption()
//     {
//         if(!currDialogue.IsLast())
//         {
//             Debug.Log("Talking!!");
//             Debug.Log("Handler thinks the message is: " + currDialogue.GetMessage());
//             uiManager.UpdateDialogue(charVis, charName, currDialogue.GetMessage());
//             currDialogue = currDialogue.GetNext();
//         }
//         else
//         {
//             uiManager.TurnOffDialogue();
//             currDialogue = currDialogueList.GetFirstDialogue();
//         }
//     }
// }





// TAKE TWO LMAO
// STRUCTURE: DIALOGUEHANDLER IS A LINKEDLIST FOR DIALOGUELIST NODES, EACH NODE HAS A LIST OF DIALOGUE OPTIONS TO CHOOSE FROM WHICH LINK TO THE NEXT DIALOGUELIST ITEM
// SO BASICALLY THE _NEXT ATTRIBUTE IS STORED IN A LIST OF DIFFERENT POSSIBILITIES CALLED DIALOGUES WHICH ALSO HAVE BOOLS AND MESSAGES AND STUFF

[System.Serializable]
public class DialogueHandler
{
    // LinkedList of different DialogueNode nodes, where each DialogueNode class is a node of LinkedList and the next node is stored in a list of Dialogue class options
    // Attributes: 
    // - originalNode: contains original node class that is the start of the conversation essentially
    // - currNode: current Dialogue node the conversation is on
    // - uiManager: uiManager object to update dialogue boxes with
    // - charName: name of character
    // - charVis: visualization sprite for the character
    // Functions:
    // - DisplayOptions(): works with UIManager to display all dialogue options and add buttons next to each one 
    // - GoToNextNode(int): calls currNode's GetNextNode() function and updates currNode = the new node
    // - ResetHandler(): resets currNode to originalNode and updates all DialogueNodes
    // - SetOriginalNode(): sets originalNode to a new node
    // - SetCurrNode(): sets currNode to a new node

    public DialogueNode originalNode;
    public DialogueNode currNode;
    public UIManager uiManager;
    public string charName;
    public Sprite charVis;
    public string playerName;
    public Sprite playerVis;
    
    public DialogueHandler(DialogueNode ogNode, UIManager ui, string cName, Sprite cVis, string pName, Sprite pVis)
    {
        originalNode = ogNode;
        currNode = ogNode;
        uiManager = ui;
        charName = cName;
        charVis = cVis;
        playerName = pName;
        playerVis = pVis;
    }

    public void DisplayOptions()
    {
        //uimanager shenanigans...?
        if(!currNode.IsCurrentlyEmpty())
        {
            currNode.UpdateAvailability();
            if(currNode.IsPlayerNode())
            {
                // if(GameObject.Find("SaveData") != null)
                // {
                //     // playerName = GameObject.Find("SaveData").GetComponent<SaveData>().GetName();
                // }
                playerName = SaveData.saveData.GetName();
                uiManager.UpdateDialogue(playerVis, playerName, currNode.GetDialogueOptions(), currNode.GetNumOptions(), true);
            }
            else
            {
                uiManager.UpdateDialogue(charVis, charName, currNode.GetDialogueOptions(), currNode.GetNumOptions(), false);   
            }
        }
        else
        {
            uiManager.TurnOffDialogue();
            currNode = originalNode;
            Debug.Log("Empty Node, Dialogue finished");
        }
    }

    public void GoToNextNode(int chosen)
    {
        //chosen must be in range of Dialogue options, or else returns and turns off dialouge and resets currNode
        if(!currNode.IsCurrentlyEmpty() && currNode.GetNumOptions() >= chosen)
        {
            currNode.GetNextDialogue(chosen).SelectedResults();
            currNode = currNode.GetNextDialogue(chosen).GetNextNode();
            DisplayOptions();
        }
        else
        {
            uiManager.TurnOffDialogue();
            currNode = originalNode;
            Debug.Log("Empty Node, Dialogue finished");

        }        
        
    }
    
}

[System.Serializable]
public class DialogueNode
{
    // Node of the DialogueHandler LinkedList containing a list of at least one dialogue option(s)
    // Attributes:
    // - DialogueList: list of Dialogue options to choose from which will also correspond to next node essentially
    // - isPlayerDialogue: boolean true if this node contains player dialogue or false if contains NPC dialogue
    // - DISREGARD: nextDefault: the default next DialogueOption used for searching through a Handler or something
    // Functions:
    // - GetNextDialogue(int): returns the chosen Dialogue option
    // - GetDialogueOptions(): returns list of dialogue messages
    // - GetNumOptions(): returns integer number of options, should be 0-4 (0 is empty, 1 is a list with only index 0)
    // - UpdateAvailability: updates Availability of all Dialogue children under this node
    // - IsCurrentlyEmpty(): returns true if current dialogueList attribute is empty or has no currently available options, false if there exists at least one Available Dialogue in dialogueList
    // - IsPlayerNode(): returns isPlayerDialogue bool
    // - AppendDialogue(Dialogue): appends dialogue to the end of dialogueList
    public List<Dialogue> dialogueList = new List<Dialogue>();
    // public Dialogue nextDefault;
    public bool isPlayerDialogue;
    

    public DialogueNode(List<Dialogue> dList, bool isPlay)
    {
        dialogueList = dList;
        // nextDefault = nextD; DISREGARD but if change mind don't forget to edit constructor parameters 
        isPlayerDialogue = isPlay;
    }

    public Dialogue GetNextDialogue(int chosen)
    {
        //chosen must be less than or equal to number of options (which should be at most four)
        int index = 1;
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                if(index == chosen)
                {
                    return d;
                }
                index += 1;
            }
        }
        Debug.Log("SOMETHING WENT WRONG NPCSTATS DIALOGUENODE GETNEXTDIALOGUE() SHOULD NOT REACH THIS POINT, ERROR WITH DIALOUGE SETUP MAYBE? OR THIS IS IMPLEMENTED WRONG AND FIX IT");
        return dialogueList[0];

    }

    public string GetDialogueOptions()
    {
        string options = "";
        // only called if dialogueList is not empty
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                options += d.GetMessage();
                options += "\n";
            }
        }
        return options;
    }

    public int GetNumOptions()
    {
        int numOptions = 0;
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                numOptions += 1;
            }
        }
        return numOptions;
    }

    public void ResetAvailability()
    {
        foreach(Dialogue d in dialogueList)
        {
            d.SetAvailability(d.startAvailable, d.startAvailable);
        }
    }

    public void UpdateAvailability()
    {
        foreach(Dialogue d in dialogueList)
        {
            d.CheckAvailabilityUpdate();
        }
    }

    public bool IsCurrentlyEmpty()
    {
        foreach(Dialogue d in dialogueList)
        {
            if(d.GetAvailability())
            {
                return false;
            }
        }
        return true;
    }

    public bool IsPlayerNode()
    {
        return isPlayerDialogue;
    }

    public void AppendDialogue(Dialogue d)
    {
        // this probably doesn't work...
        dialogueList.Add(d);
    }
}

[System.Serializable]
public class Dialogue
{
    // A class representing a single dialogue option, which may be either an NPC dialogue option or a character dialogue option
    // Attributes:
    // - nextNode: DialogueNode class containing the next linked DialogueNode node
    // - message: string with the message to be displayed 
    // - startAvailable: boolean true if starts available, false if starts unavailable
    // - currentlyAvailable: boolean true if currently available, false if not
    // - maliceResult: integer by default 0 representing any added malice from choosing this option
    // - honorResult: integer by default 0 representing any added honor from choosing this option
    // - kindnessResult: integer by default 0 representing any added kindness for choosing this option 
    // Functions
    // - UpdateAvailability: updates currentlyAvailable and/or startAvailable based off of various parameters 
    // - GetAvailability: returns true if currentlyAvailable and false otherwise
    // - GetMessage(): returns string of the message of this Dialogue option
    // - SelectedResults(): updates malice, honor, and kindness in QuestManager, as well as any other consequences that arise
    // - GetNextNode(): returns nextNode object

    // Attributes
    public DialogueNode nextNode;
    public string message;
    public bool startAvailable;
    public bool currentlyAvailable;
    public int maliceResult;
    public int honorResult;
    public int kindnessResult;
    public string doSomething; // "Do nothing" by default, set to something else if selecting this dialogue does something important
    public string availabilityState; // "Default" by default, set to something else if the availability of the Dialogue option depends on some condition (like malice honor kindness etc)
    public NPCStats parentNPC; // null by default, only used for certain dialogue options

    // Constructors 
    public Dialogue(DialogueNode dNode, string m, bool startA, int malice, int honor, int kindness, string actionToDo, string availabilityCondition, NPCStats parent)
    {
        nextNode = dNode;
        message = m;
        startAvailable = startA;
        currentlyAvailable = startA;
        maliceResult = malice;
        honorResult = honor;
        kindnessResult = kindness;
        doSomething = actionToDo;
        availabilityState = availabilityCondition;
        parentNPC = parent;
    }
    
    public Dialogue(DialogueNode dNode, string m, bool startA)
    {
        nextNode = dNode;
        message = m;
        startAvailable = startA;
        currentlyAvailable = startA;
        maliceResult = 0;
        honorResult = 0;
        kindnessResult = 0;
        doSomething = "Do nothing";
        availabilityState = "Default"; 
    }

    // Methods
    public void SetAvailability(bool setStart, bool setCurr)
    {
        // if(toggleStart)
        // {
        //     startAvailable = !startAvailable;
        // }
        // if(toggleCurr)
        // {
        //     currentlyAvailable = !currentlyAvailable;
        // }
        startAvailable = setStart;
        currentlyAvailable = setCurr;
    }

    public void CheckAvailabilityUpdate()
    {
        if(availabilityState == "Default")
        {
            Debug.Log("available by default :)");
        }
        else if(availabilityState == "Slightly mean")
        {
            SetAvailability(startAvailable, SaveData.saveData.GetMalice() >= 5);
        }
        else if(availabilityState == "Check knows allergy")
        {
            SetAvailability(startAvailable, SaveData.saveData.GetKnowsAllergy());
        }
        else if (availabilityState == "Slightly honorable")
        {
            SetAvailability(startAvailable, SaveData.saveData.GetHonor() >= 5);
        }
        else if(availabilityState == "Check has meat")
        {
            SetAvailability(startAvailable, SaveData.saveData.GetHasMeat());
        }
    }

    public bool GetAvailability()
    {
        return currentlyAvailable;
    }

    public string GetMessage()
    {
        return message;
    }

    public void SelectedResults()
    {
        // idk smth with questManager that doesn't exist yet and malice honor kindness
        if(doSomething == "Do nothing")
        {
            Debug.Log("Nothing to do");
        }
        else if(doSomething == "Turn this option off temporarily")
        {
            SetAvailability(startAvailable, false);
        }
        else if(doSomething == "Do malice/honor/kindness once")
        {
            SaveData.saveData.AddMalice(maliceResult);
            SaveData.saveData.AddHonor(honorResult);
            SaveData.saveData.AddKindness(kindnessResult);
            maliceResult = 0;
            honorResult = 0;
            kindnessResult = 0;
        }
        else if(doSomething == "Add Item")
        {
            if(!parentNPC.GetAssociatedItemGiven())
            {
                parentNPC.inventoryManager.AddItemToInventory(parentNPC.associatedItem);
                SaveData.saveData.AddItemToStarters(parentNPC.associatedItem);
                parentNPC.SetAssociatedItemGiven(true);
            }
        }
        else if(doSomething == "Update message based on honor (Sir Borro)")
        {
            if(SaveData.saveData.GetHonor() >= 3)
            {
                message = "I'm afraid I must reserve that information for trustworthy ears. You never know where your enemies lie... ";
            }
            else
            {
                message = "I hunt all manner of beasts born of the darkness. I have vanquished ghosts, ghouls, vampires, demons, and many other malicious spirits from beyond. Of late I have heard tell of a werewolf in these parts, and a sorcerer of great power besides. They have been terrorizing the good folk hereabouts with curses and bloodshed, and I am come to give them what is rightly owed. But hush, keep silent, beware of unwanted ears listening in... trust no one in this inn. No one.";
            }
        }
        else if(doSomething == "Update allergy knowledge")
        {
            SaveData.saveData.SetKnowsAllergy(true);
        }
    }

    public DialogueNode GetNextNode()
    {
        return nextNode;
    }
}
