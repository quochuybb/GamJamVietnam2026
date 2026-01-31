VAR overload_meter = 0.0

EXTERNAL SetSpriteState(stateName) 

EXTERNAL SetNotebookActive(isActive)      
EXTERNAL SetMedicalRecordActive(isActive) 


-> Start_Interaction

=== Start_Interaction ===
// [Patient Step In]
~ SetSpriteState("Mask_Happy") 

// [Doctor Greetings]
Patient: "Chào Bác sĩ nha!"
Doctor: "Chào cậ..."

// Bệnh nhân ngắt lời, nói nhanh
Patient: "Thôi khám lẹ đi."
Patient: "Tui bị gia đình ép đến đây."
Patient: "Chả hiểu sao tại sao nữa?"
Patient: "Họ mới là người có vấn đề nếu như nghĩ tui cần đến đây."
Patient: "Nè ông nghe gì không?"
Patient: "Bắt đầu khám đi chứ?"

// [Medical Record Appear] -> Mở ra cho người chơi thấy list triệu chứng
~ SetMedicalRecordActive(true) 
Bác sĩ: (Đọc danh sách triệu chứng và quan sát...)
~ SetMedicalRecordActive(false)

// [Notebook Appear]
~ SetNotebookActive(true)
Bác sĩ: (Chuẩn bị ghi chép...)
~ SetNotebookActive(false)

-> Surface_Story_Stage

=== Surface_Story_Stage ===
// Giai đoạn 1: Tìm hiểu lý do
Doctor: (Chọn câu hỏi tiếp cận)

+ [A. "Phải có lí do gì đó gia đình mới đưa anh đến đây?"]
    -> Surface_Choice_A
+ [B. "Chúng ta cố gắng xong sớm. Anh kể thêm về gia đình được không?"]
    -> Surface_Choice_B
+ [C. "Tôi thấy anh ổn mà. Anh kể thêm về bản thân anh không?"]
    -> Surface_Choice_C

= Surface_Choice_A
    // Lựa chọn sai -> Tăng Stress (Giảm overload theo hướng tiêu cực) hoặc tăng Overload tùy logic game
    // Ở đây theo logic cũ của bạn: Mad -> -10
    ~ SetSpriteState("anger")
    ~ overload_meter = overload_meter - 10
    Patient: "Có cái ***, tôi là người duy nhất trong căn nhà đó còn tỉnh táo, và chả ai chịu nghe tôi nói gì cả."
    -> Middle_Story_Stage

= Surface_Choice_B
    // Lựa chọn đúng -> Happy -> +0.5
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Ờ thì bố tôi là một doanh nhân vì may mắn mà thành công..."
    Patient: "Còn mẹ tôi á, việc duy nhất mà bà ấy làm được là đẻ được tôi."
    -> Middle_Story_Stage

= Surface_Choice_C
    // Lựa chọn đúng -> Happy -> +0.5
    ~ SetSpriteState("happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Tôi á, là thành tựu lớn nhất mà cả dòng họ tôi có thể đạt được."
    Patient: "Anh có lẽ đã nghe tới tên của bức tranh của tôi rồi. Nó có lẽ đang được bán với giá ngang tranh của Picasso."
    -> Middle_Story_Stage

=== Middle_Story_Stage ===
// [Notebook Appear]
~ SetNotebookActive(true)
Bác sĩ: (Ghi chú...)
~ SetNotebookActive(false)

Patient: "Anh biết không, tôi có thể vẽ rất nhiều bức tranh trong nhiều ngày liên tục..."
Patient: "Một ngày kéo dài tận 24 tiếng cơ mà, phải tận dụng nốt 24 tiếng đó chứ."

Doctor: (Chọn câu hỏi đào sâu)

+ [A. "Làm nhiều hơn người khác là hơn người khác à?"]
    -> Middle_Choice_A
+ [B. "Um, vậy thì anh không ngủ à?"]
    -> Middle_Choice_B
+ [C. "Anh vẽ gì trong thời gian đó? Chắc là tuyệt phẩm nhỉ?"]
    -> Middle_Choice_C

= Middle_Choice_A
    ~ SetSpriteState("Disgust")
    ~ overload_meter = overload_meter - 10
    Patient: "Anh đang ganh tị với tôi à?"
    Patient: "8 tiếng của anh chỉ để nằm như một kẻ vô dụng. Trong khi đó tôi có thể thức và vượt qua anh cả ngàn lần."
    -> End_Story_Stage

= Middle_Choice_B
    ~ SetSpriteState("Happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Đương nhiên, ai cần ngủ chứ!"
    -> End_Story_Stage

= Middle_Choice_C
    ~ SetSpriteState("Happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Đương nhiên! Tôi đã vẽ ra những bức tranh nơi mà Chúa cũng muốn mua một bức vì sự tuyệt vời của nó."
    -> End_Story_Stage

=== End_Story_Stage ===
// [Notebook Appear]
~ SetNotebookActive(true)
Bác sĩ: (Ghi chú...)
~ SetNotebookActive(false)

Patient: "Để tôi kể anh nghe vì sao tôi được sinh ra..."
Patient: "Để cứu rỗi cái nền mĩ thuật đang bị vụn vỡ này. Nhưng mọi người quá ngu muội để có thể hiểu được."

Doctor: (Chọn câu hỏi chốt hạ)

+ [A. "Anh lấy đâu ra tự tin đấy? Tôi còn chả biết anh vẽ gì."]
    -> End_Choice_A
+ [B. "Đối với anh thế nào là nghệ thuật?"]
    -> End_Choice_B
+ [C. "Vậy anh đã cứu rỗi những gì rồi?"]
    -> End_Choice_C

= End_Choice_A
    ~ SetSpriteState("Disgust")
    ~ overload_meter = overload_meter - 10
    Patient: "Vậy là anh cũng giống những con người thấp kém đó luôn gọi tôi là lười biếng..."
    Patient: "Những người như anh sẽ không bao giờ hiểu được vẻ đẹp do tôi tạo ra."
    -> Ready_For_Diagnose

= End_Choice_B
    ~ SetSpriteState("Happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Mĩ thuật là sự đổi mới, là tiến bộ, chúng ta không thể cứ cổ hủ như thế mãi được."
    -> Ready_For_Diagnose

= End_Choice_C
    ~ SetSpriteState("Happy")
    ~ overload_meter = overload_meter + 0.5
    Patient: "Tôi đã cứu vô số kể."
    Patient: "Nghệ thuật của tôi có thể cứu và lưu lại toàn bộ nỗ lực của nhân loại, thậm chí cải thiện nó và làm nó tốt hơn trước đó ngàn lần."
    -> Ready_For_Diagnose

=== Ready_For_Diagnose ===
// Ra lệnh cho Unity mở bảng chốt kết quả
~ SetNotebookActive(true)
>>> START_DIAGNOSIS
-> END