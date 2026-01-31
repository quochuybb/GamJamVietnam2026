VAR overload_meter = 0.0

EXTERNAL SetSpriteState(stateName) 
EXTERNAL ChangeBossSound(isActive) 

-> Start_Interaction

=== Start_Interaction ===
~ SetSpriteState("happy") 

Patient: "Chào bác sĩ! Trời ơi, nghe danh đã lâu nay mới gặp, bác sĩ trẻ mà phong độ quá ha! Y hệt ông nhà tui hồi xưa. À, tui có ít cam vườn nhà biếu bác sĩ lấy thảo."

Patient: "Tui đến đây cũng ngại lắm, tại tui thấy mình khỏe re à. Nhưng mà... dạo này ông nhà tui ổng kì cục lắm bác sĩ ơi."
Patient: "Ổng dạo này sao mà ổng lầm lì, tui hỏi gì cũng không nói. Bác sĩ biết ổng bị gì không?"

Doctor: "Chào chị Trinh. Cảm ơn chị về túi cam. Chị nói anh nhà dạo này ít nói? Cụ thể là như thế nào?"

-> Surface_Story_Stage

=== Surface_Story_Stage ===
Patient: "Thì nè, đi chợ về tui chào ổng cũng không thèm gật đầu. Cái mặt cứ trơ ra. Tui nghi ổng có bà nào khác ở ngoài quá. Mấy nay tui với ổng cứ lạnh tanh, không có chút lửa tình nào hết trơn á."

+ [A. "Bình tĩnh nào chị. Có thể anh ấy mệt thôi. Chị cảm thấy thế nào về sự thay đổi này?"]
    -> Surface_Choice_A
+ [B. "Chị cứ tiếp tục đi. Tôi muốn nghe thêm về tình trạng hàng ngày của chồng chị."]
    -> Surface_Choice_B
+ [C. "Lạnh nhạt? Chị nghi ngờ ngoại tình thật sao? Có bằng chứng gì không?"]
    -> Surface_Choice_C

= Surface_Choice_A
    ~ SetSpriteState("sad")
    Patient: "Tui... Tự nhiên tui thấy buồn hiu à, mà không biết sao lại buồn. Cứ như tui đã làm sai cái gì đó... Chắc tại tui nên ổng mới vậy."
    -> Middle_Story_Stage

= Surface_Choice_B
    ~ SetSpriteState("sad")
    Patient: "Thì ngày nào cũng vậy, ổng cứ ngồi trên cái ghế bành đó, còn chả thèm nhìn tui lấy một cái."
    -> Middle_Story_Stage

= Surface_Choice_C
    ~ SetSpriteState("sad")
    Patient: "Thì tối ngủ mà ổng để tui ngủ mình ên à, không có ôm tui gì nữa hết trơn. Chắc chắn là chê tui già rồi!"
    -> Middle_Story_Stage

=== Middle_Story_Stage ===
~ SetSpriteState("angry")
Patient: "Đã vậy nha, dạo này cơm nước tui nấu ngon lành mà ổng không thèm đụng đũa. Cứ ngồi nhìn chằm chằm vào bát cơm. Tui tức quá, tui phải đút ổng mới chịu ăn đó bác sĩ!"

+ [A. "Chị chịu khó chăm sóc anh ấy thật. Có vẻ anh ấy vẫn ăn uống được là tốt rồi."]
    -> Middle_Choice_A
+ [B. "Chị kể thêm về việc ăn uống của anh ấy xem."]
    -> Middle_Choice_B
+ [C. "Chị đút như thế nào cơ? Anh ấy có tự nhai được không? Anh nhà bị bệnh à?"]
    -> Middle_Choice_C

= Middle_Choice_A
    ~ SetSpriteState("happy")
    Patient: "Ừa thì cũng ráng thôi. Mà bác sĩ nói đúng, dạo này trộm vía da thịt ổng cứng cáp hẳn lên. Chắc do tui tẩm bổ tốt."
    -> End_Story_Stage

= Middle_Choice_B
    ~ SetSpriteState("disgust")
    Patient: "Thì bữa nào cũng phải ép. Đàn ông gì mà nhõng nhẽo như con nít."
    -> End_Story_Stage

= Middle_Choice_C
    ~ SetSpriteState("happy")
    Patient: "Thì... thì tui banh miệng ổng ra! Tui nhét cơm vào! Phải lấy tay vuốt cổ ổng mới chịu nuốt. Ổng làm nũng tui đó mà!"
    -> End_Story_Stage

=== End_Story_Stage ===
    ~ChangeBossSound(true)
    
Patient: "À mà bác sĩ ơi, cái này mới lạ nè. Mấy đêm nay ổng không ngủ. Tui kéo ổng vô buồng, đặt lưng xuống mà mắt ổng cứ mở trân trân nhìn lên trần nhà."
Patient: "Tui vuốt mắt hoài mà ổng cứ mở lại. Ổng giận tui cái gì không biết? Tui có làm gì sai đâu."

Doctor: "..."

+ [A. "Chị Trinh này... Có lẽ anh ấy đã đi đến một nơi rất xa rồi. Chị hiểu ý tôi không?"]
    -> End_Choice_A
+ [B. "....Nghe có vẻ nghiêm trọng đấy…. Ch-Chị nên về nhà kiểm tra kỹ lại xem. Có lẽ anh ấy đang bệnh nặng!??"]
    -> End_Choice_B
+ [C. "Chị Trinh! Chồng chị chết rồi! Chết không nhắm mắt! Chị đang làm cái trò gì với cái xác vậy??"]
    -> End_Choice_C

= End_Choice_A
    ~ SetSpriteState("sad")
    Patient: "Đi xa...? Ý bác sĩ là... ổng bỏ tui đi thật hả? Hèn chi... hèn chi người ổng lạnh quá bác sĩ ơi..."
    Patient: "Ông ơi... sao ông bỏ tui nằm lại đó một mình... tui biết sống sao đây..."
    Doctor: "Chị Trinh đã gục ngã khi nhận ra sự thật. Tôi đã gọi đội ngũ y tế đưa chị đi chăm sóc đặc biệt."
    >>> TRUE_ENDING
    -> Ready_For_Diagnose

= End_Choice_B
    ~ SetSpriteState("surprise")
    Patient: "Bệnh nặng hả bác sĩ? Chết cha, để tui về coi ổng liền? Dạ tui cảm ơn bác sĩ nhiều, tui về gấp đây!"
    
    Doctor: "Chị ấy hối hả rời đi. Tôi cảm thấy có điều gì đó không ổn và cần phải đưa ra quyết định."
    
    + [Hành động: Gọi điện báo cảnh sát hỗ trợ]
    Doctor: "Cảnh sát đã đến kịp thời. Họ phát hiện thi thể người chồng đã phân hủy. Chị Trinh được đưa đi điều trị tâm thần bắt buộc."
    >>> TRUE_ENDING_POLICE
        -> Ready_For_Diagnose
        
    + [Hành động: Để chị ấy tự lo liệu]
        Doctor: "Tôi đã không làm gì. Vài ngày sau, người ta tìm thấy chị Trinh đã tự tử tại nhà để được bên cạnh chồng mình."
        >>> BAD_ENDING_SUICIDE
        -> Ready_For_Diagnose

= End_Choice_C
    ~ SetSpriteState("surprise")
    Patient: "KHÔNG!!! Ông nói bậy! Ổng còn sống! Ổng đang đợi tui về nấu cơm! Đồ bác sĩ lừa đảo!"
    
    Doctor: "Chị ấy hét lên rồi lao ra khỏi phòng khám. Một tiếng va chạm lớn vang lên ngay sau đó."
    Doctor: "Chị Trinh đã tử vong do tai nạn giao thông trong lúc hoảng loạn. Một kết thúc thật đau lòng."
    >>> BAD_ENDING_ACCIDENT
    -> Ready_For_Diagnose

=== Ready_For_Diagnose ===
Doctor: "Cuộc gặp gỡ đã kết thúc. Bây giờ tôi cần ghi lại chẩn đoán chính xác nhất về tình trạng của chị ấy."
>>> START_DIAGNOSIS
-> END