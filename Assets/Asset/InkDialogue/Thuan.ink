VAR overload_meter = 0.0

EXTERNAL SetSpriteState(stateName) 
    
-> Start_Interaction

=== Start_Interaction ===
// [Patient Step In]
// Narrative: (Cửa mở mạnh. Bệnh nhân bước vào với dáng vẻ vội vã, mắt sáng rực, nhìn quanh phòng liên tục)
~ SetSpriteState("Mask_Happy") 

// [Doctor Greetings - First Impression]
Patient: "Chào Bác sĩ! Wow, phòng khám này... hơi ảm đạm nhỉ? Bác sĩ nên sơn lại tường màu vàng chanh, hoặc đỏ rực!"
Patient: "Nhưng không sao, tôi đang cảm thấy tuyệt vời. Chưa bao giờ tốt hơn! Bố mẹ tôi cứ làm quá lên rồi bắt tôi đến đây thôi."
// (⇒ E2: Core Symptom: Elevated Mood)

Doctor: "Cậu có vẻ đang rất vui vẻ. Hãy cho tôi biết thêm về bản thân cậu được không?"

// [Medical Record Appear]: N.C Thuận, 20 tuổi, Họa sĩ, Con nhà giàu
// [Notebook Appear]

-> Surface_Story_Stage

=== Surface_Story_Stage ===
// [Player close Notebook]
Patient: "Tôi là họa sĩ, nhưng không phải kiểu họa sĩ bình thường đâu. Mấy ngày nay, ý tưởng trong đầu tôi cứ nổ 'bùm bùm bùm' liên tục!"
Patient: "Tôi vẽ không ngừng nghỉ. Màu sắc đang nói chuyện với tôi, bác sĩ ạ. Tôi cảm thấy mình đang kết nối với vũ trụ!"
// (⇒ C4: Racing Thoughts, E2: Elevated Mood)

+ [A. "Nghe có vẻ cậu đang làm việc rất vất vả. Cậu có nghỉ ngơi chút nào không?"]
    -> Surface_Choice_A
+ [B. "Cậu đã vẽ được bao nhiêu bức tranh trong tuần này rồi?"]
    -> Surface_Choice_B
+ [C. "Kết nối với vũ trụ sao? Nghe thật phi thường! Kể chi tiết hơn đi!"]
    -> Surface_Choice_C

= Surface_Choice_A
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Nghỉ ngơi? Ai cần ngủ chứ? Ngủ là phí phạm thời gian!"
    // (⇒ B4: Decreased need for sleep)
    -> Middle_Story_Stage

= Surface_Choice_B
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Hàng chục! Không, hàng trăm! Tôi không đếm xuể!"
    // (⇒ C4: Racing Thoughts)
    -> Middle_Story_Stage

= Surface_Choice_C
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Đúng thế! Mọi giác quan của tôi đều được mở khóa. Tôi thấy những thứ người thường không thấy!"
    -> Middle_Story_Stage

=== Middle_Story_Stage ===
// [Notebook Appear]
Patient: "Thực ra, tôi nhận ra mình không chỉ là họa sĩ. Tôi là một thiên tài bị lãng quên của thế kỷ 21."
Patient: "Picasso? Van Gogh? Họ chả là gì so với những gì tôi sắp tạo ra. Tôi cảm thấy mình có sứ mệnh thay đổi cả thế giới này."
// (⇒ C7: Grandiosity)

+ [A. "Đó là một mục tiêu lớn. Nhưng chúng ta hãy quay lại vấn đề sức khỏe hiện tại nhé."]
    -> Middle_Choice_A
+ [B. "Gia đình cậu nghĩ sao về những tác phẩm mới này?"]
    -> Middle_Choice_B
+ [C. "Vậy anh đã vẽ những gì trong quảng thời gian đó, chắc là những tác phẩm tuyệt vời nhỉ?"]
    -> Middle_Choice_C

= Middle_Choice_A
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Sức khỏe tôi hoàn hảo! Tôi khỏe hơn bất kỳ ai!"
    // (⇒ B5: Excess Energy)
    -> End_Story_Stage

= Middle_Choice_B
    ~ SetSpriteState("angry")
    ~ overload_meter = overload_meter - 5
    Patient: "Họ không hiểu! Họ là những người trần mắt thịt nhàm chán."
    -> End_Story_Stage

= Middle_Choice_C
    ~ SetSpriteState("angry")
    ~ overload_meter = overload_meter - 10
    Patient: "Bác sĩ nghi ngờ tôi sao? Ông cũng giống hệt bọn họ, ghen tị với tài năng của tôi!"
    // (⇒ E5: Irritability)
    -> End_Story_Stage

=== End_Story_Stage ===
// [Notebook Appear]
Patient: "Mà thôi, không quan trọng. Sáng nay tôi vừa quẹt thẻ của ông già mua lại một xưởng tranh cũ giá 2 tỷ."
Patient: "Tôi chưa xem nó nát thế nào, nhưng tôi thích thì tôi mua thôi. Tiền bạc chỉ là giấy lộn so với nghệ thuật của tôi!"
// (⇒ B2: Excessive involvement in activities with high potential for painful consequences)

+ [A. "2 tỷ là số tiền rất lớn. Cậu có bàn bạc với ai trước khi mua không?"]
    -> End_Choice_A
+ [B. "Cậu định làm gì với xưởng tranh đó?"]
    -> End_Choice_B
+ [C. "Cậu vừa ném 2 tỷ qua cửa sổ mà không suy nghĩ sao? Cậu điên rồi!"]
    -> End_Choice_C

= End_Choice_A
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Bàn bạc? Ha! Bàn bạc là dành cho những kẻ thiếu quyết đoán. Bố tôi có cả núi tiền, tôi chỉ đang giúp ông ấy tiêu bớt cho mục đích cao cả thôi."
    Patient: "Đợi đến khi nó thành bảo tàng, ông ấy sẽ phải quỳ xuống cảm ơn tôi!"
    // (⇒ C7: Grandiosity)
    -> Ready_For_Diagnose

= End_Choice_B
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Nó sẽ là thánh địa của nghệ thuật mới! Tôi sẽ vẽ lên trần nhà, lên sàn nhà, thậm chí thuê người về chỉ để đứng đó làm tượng."
    Patient: "Mọi người sẽ xếp hàng dài cả cây số để được vào xem! Bác sĩ sẽ là khách VIP, tôi hứa đấy!"
    // (⇒ C4: Racing Thoughts)
    -> Ready_For_Diagnose

= End_Choice_C
    ~ SetSpriteState("angry")
    ~ overload_meter = overload_meter - 20
    Patient: "Điên? Ông gọi thiên tài là điên sao? Đúng là tầm nhìn hạn hẹp! Ông cũng giống hệt bọn họ."
    Patient: "Tôi không cần ai dạy khôn cả. Đừng làm mất thời gian của tôi nữa!"
    -> Ready_For_Diagnose

=== Ready_For_Diagnose ===
// Ra lệnh cho Unity mở bảng chốt kết quả chẩn đoán
>>> START_DIAGNOSIS
-> END
