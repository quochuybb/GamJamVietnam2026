VAR overload_meter = 0.0

EXTERNAL SetSpriteState(stateName) 
    
-> Start_Interaction

=== Start_Interaction ===
// [Patient Step In]
// Narrative: (Bệnh nhân bước vào, kéo ghế nhưng không ngồi yên, tay xoắn vào nhau, mắt nhìn xuống đất nhưng thỉnh thoảng liếc nhanh lên trần nhà)
~ SetSpriteState("fearful") 

Patient: "Tôi... tôi không biết tại sao mình lại ở đây. Đầu tôi cứ như có sương mù ấy. Tôi cảm thấy... lạc lõng. Giống như tôi đang trôi đi mà không ai tóm lại được."

Doctor: "Chào cô. Trông cô có vẻ rất lo lắng. Hãy hít thở sâu và kể cho tôi nghe chuyện gì đang xảy ra."

// [Medical Record Appear]: Bằng, 28 tuổi, Bác sĩ thú y. Lớn lên tại trại trẻ mồ côi.
// [Notebook Appear]

-> Surface_Story_Stage

=== Surface_Story_Stage ===
Patient: "Cuộc đời tôi... thật sự là một mớ hỗn độn vô nghĩa. Sáng thức dậy, tôi tự hỏi tại sao mình phải tỉnh lại? Tôi thấy mình vô dụng, thừa thãi."
Patient: "Nhiều khi tôi ước mình cứ thế mà tan biến đi cho rồi."
// (⇒ C6 - Hopeless Thinking)

+ [A. "Bình tĩnh nào, không ai là vô dụng cả. Cô đã nỗ lực rất nhiều để trở thành bác sĩ thú y mà."]
    -> Surface_Choice_A
+ [B. "Ý cô là sao khi nói cô 'vô dụng'? Cô đang có ý định làm hại bản thân ư?"]
    -> Surface_Choice_B
+ [C. "Ờ... Có vẻ cô đang suy nghĩ quá nhiều rồi."]
    -> Surface_Choice_C

= Surface_Choice_A
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Anh nghĩ vậy thật à? Tạ ơn trời! Cảm ơn anh! Anh nói đúng, tôi là bác sĩ mà, tôi cứu sống bao nhiêu sinh mạng cơ mà!"
    // (⇒ E2 - Elevated Mood / Chuyển trạng thái hưng phấn nhanh)
    -> Middle_Story_Stage

= Surface_Choice_B
    ~ SetSpriteState("sadness")
    ~ overload_meter = overload_meter + 0.1
    Patient: "Thì là vô dụng chứ sao! Chả muốn làm gì cả. Chỉ muốn nằm lì một chỗ cho đất vùi lấp đi."
    // (⇒ C6 - Hopeless Thinking)
    -> Middle_Story_Stage

= Surface_Choice_C
    ~ SetSpriteState("anger")
    ~ overload_meter = overload_meter - 10.0
    Patient: "Ờ là sao? Ý anh là gì? Anh cũng nghĩ tôi là rác rưởi vô dụng đúng không? Tôi biết ngay mà!"
    // (⇒ E5 - Irritability)
    -> Middle_Story_Stage

=== Middle_Story_Stage ===
// [Notebook Appear]
~ SetSpriteState("happy")
Patient: "Nhưng mà anh biết không... dạo này ở phòng khám, mọi thứ cứ vụt vụt vụt qua mặt tôi. Tôi mổ cho chó mèo mà tay tôi làm nhanh hơn cả não."
Patient: "Đồng nghiệp cứ kêu tôi chậm lại, bảo tôi nghỉ ngơi đi. Nhưng tôi thấy có cần thiết đâu? Tôi đang sung sức mà!"
// (⇒ B5 - Excess Energy / C4 - Racing Thoughts)

+ [A. "Những chuyện sai sót có thể xảy ra khi làm quá nhanh. Cô chỉ cần chú ý chậm lại một chút là được."]
    -> Middle_Choice_A
+ [B. "Cô thực sự cần nghỉ ngơi đấy, nhìn mắt cô thâm quầng tiều tụy lắm rồi."]
    -> Middle_Choice_B
+ [C. "Sao cô không lắng nghe những người đồng nghiệp của cô đi?"]
    -> Middle_Choice_C

= Middle_Choice_A
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Thật ư? Anh nói chuyện dễ nghe ghê. Cảm giác thật thoải mái quá đi! Mà này... anh nhìn kỹ xem, hôm nay tôi có đẹp không?"
    // (⇒ E2 - Elevated Mood)
    -> End_Story_Stage

= Middle_Choice_B
    ~ SetSpriteState("sadness")
    ~ overload_meter = overload_meter + 0.2
    Patient: "Tôi muốn nằm xuống lắm chứ, nhưng cái đầu tôi nó không có công tắc tắt! Nó cứ chạy cả đêm, làm sao mà ngủ?"
    // (⇒ B4 - Sleep Disturbance)
    -> End_Story_Stage

= Middle_Choice_C
    ~ SetSpriteState("anger")
    ~ overload_meter = overload_meter - 5.0
    Patient: "Nghe họ? Làm sao tôi nghe được khi họ nói quá chậm? Trong lúc họ rặn ra được một chữ thì trong đầu tôi đã nảy ra mười ý tưởng rồi!"
    // (⇒ C4 - Racing Thoughts)
    -> End_Story_Stage

=== End_Story_Stage ===
// [Notebook Appear]
~ SetSpriteState("sadness")
Patient: "Hồi đầu tôi thương bọn động vật lắm, con nào đau là tôi khóc theo. Nhưng dạo gần đây... tôi nhìn chúng nó giãy chết trước mặt mà lòng tôi… chả cảm thấy gì cả."
Patient: "Cứ như tôi đang mổ xẻ mấy con gấu bông vô tri vậy."
// (⇒ E1 - Emotional Numbness)

+ [A. "Nhưng đó là công việc của cô mà? Cô không cảm thấy chút thương xót nào sao?"]
    -> End_Choice_A
+ [B. "Cô chỉ cần chịu khó tí thôi, chúng nó cũng dễ thương mà đúng không?"]
    -> End_Choice_B
+ [C. "Thì chúng nó chỉ là động vật thôi mà, cô cao cấp hơn chúng nó nhiều nên không cần lo."]
    -> End_Choice_C

= End_Choice_A
    ~ SetSpriteState("sadness")
    Patient: "Thương xót? Tôi cố tìm cái cảm giác đó mà không thấy đâu cả. Tôi nhìn vào mắt chúng nó và chỉ thấy... trống rỗng. Máu chảy hay tim ngừng đập, với tôi giờ chẳng khác gì nhau."
    // (⇒ E1 - Emotional Numbness)
    -> Ready_For_Diagnose

= End_Choice_B
    ~ SetSpriteState("sadness")
    Patient: "Dễ thương? Chả bù cho tôi. Tôi nhìn lại mình trong gương và thấy một đống rác rưởi. Tôi không xứng đáng làm bác sĩ. Tôi nên biến mất để đỡ chật đất."
    // (⇒ C6 - Hopeless Thinking)
    -> Ready_For_Diagnose

= End_Choice_C
    ~ SetSpriteState("happy")
    Patient: "Đúng! Anh nói đúng! Tôi nắm quyền sinh sát trong tay mà. Sao tôi phải buồn vì mấy con vật cỏn con đó chứ? Tôi tuyệt vời hơn thế nhiều!"
    // (⇒ E2 - Elevated Mood / Tự tôn cao)
    -> Ready_For_Diagnose

=== Ready_For_Diagnose ===
Doctor: "Bằng dường như đang mắc kẹt giữa hai thái cực cảm xúc đối lập. Đã đến lúc đưa ra kết luận."
>>> START_DIAGNOSIS
-> END