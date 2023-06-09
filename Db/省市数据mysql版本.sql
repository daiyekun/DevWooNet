-- ----------------------国家begin--------------------------------------------------------------------------------
SET FOREIGN_KEY_CHECKS = 0;

INSERT INTO `dev_country` VALUES (1, "中国", "中国", 1);
INSERT INTO `dev_country`  VALUES (2, "海外", "海外", 1);
SET FOREIGN_KEY_CHECKS = 1;
-- --------------------国家-end-----------------------------------------------------------------------------------

------------------------34省--begin 执行少数据分段执行------------------------------------------------------------
SET FOREIGN_KEY_CHECKS = 0;

INSERT INTO `dev_province` VALUES (1, 1, "北京","北京", 1);
INSERT INTO `dev_province` VALUES (2, 1, "上海","上海", 1);
INSERT INTO `dev_province` VALUES (3, 1, "天津","天津", 1);
INSERT INTO `dev_province` VALUES (4, 1, "重庆","重庆", 1);
INSERT INTO `dev_province` VALUES (5, 1, "安徽","安徽", 1);
INSERT INTO `dev_province` VALUES (6, 1, "福建","福建", 1);
INSERT INTO `dev_province` VALUES (7, 1, "甘肃","甘肃", 1);
INSERT INTO `dev_province` VALUES (8, 1, "广东","广东", 1);
INSERT INTO `dev_province` VALUES (9, 1, "广西","广西", 1);
INSERT INTO `dev_province` VALUES (10, 1, "贵州","贵州", 1);
INSERT INTO `dev_province` VALUES (11, 1, "海南","海南", 1);
INSERT INTO `dev_province` VALUES (12, 1, "河北","河北", 1);
INSERT INTO `dev_province` VALUES (13, 1, "河南","河南", 1);
INSERT INTO `dev_province` VALUES (14, 1, "黑龙江","黑龙江",1);
INSERT INTO `dev_province` VALUES (15, 1, "湖北","湖北", 1);
INSERT INTO `dev_province` VALUES (16, 1, "湖南","湖南", 1);
INSERT INTO `dev_province` VALUES (17, 1, "吉林","吉林", 1);
INSERT INTO `dev_province` VALUES (18, 1, "江苏","江苏", 1);
INSERT INTO `dev_province` VALUES (19, 1, "江西","江西", 1);
INSERT INTO `dev_province` VALUES (20, 1, "辽宁","辽宁", 1)
INSERT INTO `dev_province` VALUES (21, 1, "内蒙古","内蒙古",1);
INSERT INTO `dev_province` VALUES (22, 1, "宁夏","宁夏", 1);
INSERT INTO `dev_province` VALUES (23, 1, "青海","青海", 1);
INSERT INTO `dev_province` VALUES (24, 1, "山东","山东", 1);
INSERT INTO `dev_province` VALUES (25, 1, "山西","山西", 1);
INSERT INTO `dev_province` VALUES (26, 1, "陕西","陕西", 1);
INSERT INTO `dev_province` VALUES (27, 1, "四川","四川", 1);
INSERT INTO `dev_province` VALUES (28, 1, "西藏","西藏", 1);
INSERT INTO `dev_province` VALUES (29, 1, "新疆","新疆", 1);
INSERT INTO `dev_province` VALUES (30, 1, "云南","云南", 1);
INSERT INTO `dev_province` VALUES (31, 1, "浙江","浙江", 1);
INSERT INTO `dev_province` VALUES (32, 1, "香港","香港", 1);
INSERT INTO `dev_province` VALUES (33, 1, "澳门","澳门", 1);
INSERT INTO `dev_province` VALUES (34, 1, "台湾","台湾", 1);
SET FOREIGN_KEY_CHECKS = 1;

-------------------------省--end-------------------------------------------------------------------------------

-------------------------市--begin-------------------------------------------------------------
SET FOREIGN_KEY_CHECKS = 0;

INSERT INTO `dev_city` VALUES (1, 1,"市辖区","市辖区", 1);;
INSERT INTO `dev_city` VALUES (2, 1,'东城区','东城区', 1);
INSERT INTO `dev_city` VALUES (3, 1,'西城区','西城区', 1);
INSERT INTO `dev_city` VALUES (4, 1,'崇文区','崇文区', 1);
INSERT INTO `dev_city` VALUES (5, 1,'宣武区','宣武区', 1);
INSERT INTO `dev_city` VALUES (6, 1,'朝阳区','朝阳区', 1);
INSERT INTO `dev_city` VALUES (7, 1,'丰台区','丰台区', 1);
INSERT INTO `dev_city` VALUES (8, 1,'石景山区','石景山区', 1);
INSERT INTO `dev_city` VALUES (9, 1,'海淀区','海淀区', 1);
INSERT INTO `dev_city` VALUES (10, 1,'门头沟区','门头沟区', 1);
INSERT INTO `dev_city` VALUES (11, 1,'房山区','房山区', 1);
INSERT INTO `dev_city` VALUES (12, 1,'通州区','通州区', 1);
INSERT INTO `dev_city` VALUES (13, 1,'顺义区','顺义区', 1);
INSERT INTO `dev_city` VALUES (14, 1,'昌平区','昌平区', 1);
INSERT INTO `dev_city` VALUES (15, 1,'大兴区','大兴区', 1);
INSERT INTO `dev_city` VALUES (16, 1,'怀柔区','怀柔区', 1);
INSERT INTO `dev_city` VALUES (17, 1,'平谷区','平谷区', 1);
INSERT INTO `dev_city` VALUES (18, 1,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (19, 2,'市辖区','市辖区', 1);
INSERT INTO `dev_city` VALUES (20, 2,'黄浦区','黄浦区', 1);
INSERT INTO `dev_city` VALUES (21, 2,'卢湾区','卢湾区', 1);
INSERT INTO `dev_city` VALUES (22, 2,'徐汇区','徐汇区', 1);
INSERT INTO `dev_city` VALUES (23, 2,'长宁区','长宁区', 1);
INSERT INTO `dev_city` VALUES (24, 2,'静安区','静安区', 1);
INSERT INTO `dev_city` VALUES (25, 2,'普陀区','普陀区', 1);
INSERT INTO `dev_city` VALUES (26, 2,'闸北区','闸北区', 1);
INSERT INTO `dev_city` VALUES (27, 2,'虹口区','虹口区', 1);
INSERT INTO `dev_city` VALUES (28, 2,'杨浦区','杨浦区', 1);
INSERT INTO `dev_city` VALUES (29, 2,'闵行区','闵行区', 1);
INSERT INTO `dev_city` VALUES (30, 2,'宝山区','宝山区', 1);
INSERT INTO `dev_city` VALUES (31, 2,'嘉定区','嘉定区', 1);
INSERT INTO `dev_city` VALUES (32, 2,'浦东新区','浦东新区', 1);
INSERT INTO `dev_city` VALUES (33, 2,'金山区','金山区', 1);
INSERT INTO `dev_city` VALUES (34, 2,'松江区','松江区', 1);
INSERT INTO `dev_city` VALUES (35, 2,'青浦区','青浦区', 1);
INSERT INTO `dev_city` VALUES (36, 2,'南汇区','南汇区', 1);
INSERT INTO `dev_city` VALUES (37, 2,'奉贤区','奉贤区', 1);
INSERT INTO `dev_city` VALUES (38, 2,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (39, 3,'市辖区','市辖区', 1);
INSERT INTO `dev_city` VALUES (40, 3,'和平区','和平区', 1);
INSERT INTO `dev_city` VALUES (41, 3,'河东区','河东区', 1);
INSERT INTO `dev_city` VALUES (42, 3,'河西区','河西区', 1);
INSERT INTO `dev_city` VALUES (43, 3,'南开区','南开区', 1);
INSERT INTO `dev_city` VALUES (44, 3,'河北区','河北区', 1);
INSERT INTO `dev_city` VALUES (45, 3,'红桥区','红桥区', 1);
INSERT INTO `dev_city` VALUES (46, 3,'塘沽区','塘沽区', 1);
INSERT INTO `dev_city` VALUES (47, 3,'汉沽区','汉沽区', 1);
INSERT INTO `dev_city` VALUES (48, 3,'大港区','大港区', 1);
INSERT INTO `dev_city` VALUES (49, 3,'东丽区','东丽区', 1);
INSERT INTO `dev_city` VALUES (50, 3,'西青区','西青区', 1);
INSERT INTO `dev_city` VALUES (51, 3,'津南区','津南区', 1);
INSERT INTO `dev_city` VALUES (52, 3,'北辰区','北辰区', 1);
INSERT INTO `dev_city` VALUES (53, 3,'武清区','武清区', 1);
INSERT INTO `dev_city` VALUES (54, 3,'宝坻区','宝坻区', 1);
INSERT INTO `dev_city` VALUES (55, 3,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (56, 4,'重庆市','重庆市', 1);
INSERT INTO `dev_city` VALUES (57, 5,'安庆','安庆', 1);
INSERT INTO `dev_city` VALUES (58, 5,'蚌埠','蚌埠', 1);
INSERT INTO `dev_city` VALUES (59, 5,'亳州','亳州', 1);
INSERT INTO `dev_city` VALUES (60, 5,'巢湖','巢湖', 1);
INSERT INTO `dev_city` VALUES (61, 5,'池州','池州', 1);
INSERT INTO `dev_city` VALUES (62, 5,'滁州','滁州', 1);
INSERT INTO `dev_city` VALUES (63, 5,'阜阳','阜阳', 1);
INSERT INTO `dev_city` VALUES (64, 5,'合肥','合肥', 1);
INSERT INTO `dev_city` VALUES (65, 5,'淮北','淮北', 1);
INSERT INTO `dev_city` VALUES (66, 5,'淮南','淮南', 1);
INSERT INTO `dev_city` VALUES (67, 5,'黄山','黄山', 1);
INSERT INTO `dev_city` VALUES (68, 5,'六安','六安', 1);
INSERT INTO `dev_city` VALUES (69, 5,'马鞍山','马鞍山', 1);
INSERT INTO `dev_city` VALUES (70, 5,'宿州','宿州', 1);
INSERT INTO `dev_city` VALUES (71, 5,'铜陵','铜陵', 1);
INSERT INTO `dev_city` VALUES (72, 5,'芜湖','芜湖', 1);
INSERT INTO `dev_city` VALUES (73, 5,'宣城','宣城', 1);
INSERT INTO `dev_city` VALUES (74, 5,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (75, 6,'福州','福州', 1);
INSERT INTO `dev_city` VALUES (76, 6,'龙岩','龙岩', 1);
INSERT INTO `dev_city` VALUES (77, 6,'南平','南平', 1);
INSERT INTO `dev_city` VALUES (78, 6,'宁德','宁德', 1);
INSERT INTO `dev_city` VALUES (79, 6,'莆田','莆田', 1);
INSERT INTO `dev_city` VALUES (80, 6,'泉州','泉州', 1);
INSERT INTO `dev_city` VALUES (81, 6,'三明','三明', 1);
INSERT INTO `dev_city` VALUES (82, 6,'厦门','厦门', 1);
INSERT INTO `dev_city` VALUES (83, 6,'漳州','漳州', 1);
INSERT INTO `dev_city` VALUES (84, 6,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (85, 7,'白银','白银', 1);
INSERT INTO `dev_city` VALUES (86, 7,'定西','定西', 1);
INSERT INTO `dev_city` VALUES (87, 7,'甘南','甘南', 1);
INSERT INTO `dev_city` VALUES (88, 7,'嘉峪关','嘉峪关', 1);
INSERT INTO `dev_city` VALUES (89, 7,'金昌','金昌', 1);
INSERT INTO `dev_city` VALUES (90, 7,'酒泉','酒泉', 1);
INSERT INTO `dev_city` VALUES (91, 7,'兰州','兰州', 1);
INSERT INTO `dev_city` VALUES (92, 7,'临夏','临夏', 1);
INSERT INTO `dev_city` VALUES (93, 7,'陇南','陇南', 1);
INSERT INTO `dev_city` VALUES (94, 7,'平凉','平凉', 1);
INSERT INTO `dev_city` VALUES (95, 7,'庆阳','庆阳', 1);
INSERT INTO `dev_city` VALUES (96, 7,'天水','天水', 1);
INSERT INTO `dev_city` VALUES (97, 7,'武威','武威', 1);
INSERT INTO `dev_city` VALUES (98, 7,'张掖','张掖', 1);
INSERT INTO `dev_city` VALUES (99, 7,'其他','其他', 1);
--------------------------------------------------------------------------------
INSERT INTO `dev_city` VALUES (100, 8,'潮州','潮州', 1);
INSERT INTO `dev_city` VALUES (101, 8,'东莞','东莞', 1);
INSERT INTO `dev_city` VALUES (102, 8,'佛山','佛山', 1);
INSERT INTO `dev_city` VALUES (103, 8,'广州','广州', 1);
INSERT INTO `dev_city` VALUES (104, 8,'河源','河源', 1);
INSERT INTO `dev_city` VALUES (105, 8,'惠州','惠州', 1);
INSERT INTO `dev_city` VALUES (106, 8,'江门','江门', 1);
INSERT INTO `dev_city` VALUES (107, 8,'揭阳','揭阳', 1);
INSERT INTO `dev_city` VALUES (108, 8,'茂名','茂名', 1);
INSERT INTO `dev_city` VALUES (109, 8,'梅州','梅州', 1);
INSERT INTO `dev_city` VALUES (110, 8,'清远','清远', 1);
INSERT INTO `dev_city` VALUES (111, 8,'汕头','汕头', 1);
INSERT INTO `dev_city` VALUES (112, 8,'汕尾','汕尾', 1);
INSERT INTO `dev_city` VALUES (113, 8,'韶关','韶关', 1);
INSERT INTO `dev_city` VALUES (114, 8,'深圳','深圳', 1);
INSERT INTO `dev_city` VALUES (115, 8,'阳江','阳江', 1);
INSERT INTO `dev_city` VALUES (116, 8,'云浮','云浮', 1);
INSERT INTO `dev_city` VALUES (117, 8,'湛江','湛江', 1);
INSERT INTO `dev_city` VALUES (118, 8,'肇庆','肇庆', 1);
INSERT INTO `dev_city` VALUES (119, 8,'中山','中山', 1);
INSERT INTO `dev_city` VALUES (120, 8,'珠海','珠海', 1);
INSERT INTO `dev_city` VALUES (121, 8,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (122, 9,'百色','百色', 1);
INSERT INTO `dev_city` VALUES (123, 9,'北海','北海', 1);
INSERT INTO `dev_city` VALUES (124, 9,'崇左','崇左', 1);
INSERT INTO `dev_city` VALUES (125, 9,'防城港','防城港', 1);
INSERT INTO `dev_city` VALUES (126, 9,'贵港','贵港', 1);
INSERT INTO `dev_city` VALUES (127, 9,'桂林','桂林', 1);
INSERT INTO `dev_city` VALUES (128, 9,'河池','河池', 1);
INSERT INTO `dev_city` VALUES (129, 9,'贺州','贺州', 1);
INSERT INTO `dev_city` VALUES (130, 9,'来宾','来宾', 1);
INSERT INTO `dev_city` VALUES (131, 9,'柳州','柳州', 1);
INSERT INTO `dev_city` VALUES (132, 9,'南宁','南宁', 1);
INSERT INTO `dev_city` VALUES (133, 9,'钦州','钦州', 1);
INSERT INTO `dev_city` VALUES (134, 9,'梧州','梧州', 1);
INSERT INTO `dev_city` VALUES (135, 9,'玉林','玉林', 1);
INSERT INTO `dev_city` VALUES (136, 9,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (137, 10,'安顺','安顺', 1);
INSERT INTO `dev_city` VALUES (138, 10,'毕节','毕节', 1);
INSERT INTO `dev_city` VALUES (139, 10,'贵阳','贵阳', 1);
INSERT INTO `dev_city` VALUES (140, 10,'六盘水','六盘水', 1);
INSERT INTO `dev_city` VALUES (141, 10,'黔东南','黔东南', 1);
INSERT INTO `dev_city` VALUES (142, 10,'黔南','黔南', 1);
INSERT INTO `dev_city` VALUES (143, 10,'黔西南','黔西南', 1);
INSERT INTO `dev_city` VALUES (144, 10,'铜仁','铜仁', 1);
INSERT INTO `dev_city` VALUES (145, 10,'遵义','遵义', 1);
INSERT INTO `dev_city` VALUES (146, 10,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (147, 11,'白沙','白沙', 1);
INSERT INTO `dev_city` VALUES (148, 11,'保亭','保亭', 1);
INSERT INTO `dev_city` VALUES (149, 11,'昌江','昌江', 1);
INSERT INTO `dev_city` VALUES (150, 11,'澄迈','澄迈', 1);
INSERT INTO `dev_city` VALUES (151, 11,'儋州','儋州', 1);
INSERT INTO `dev_city` VALUES (152, 11,'定安','定安', 1);
INSERT INTO `dev_city` VALUES (153, 11,'东方','东方', 1);
INSERT INTO `dev_city` VALUES (154, 11,'海口','海口', 1);
INSERT INTO `dev_city` VALUES (155, 11,'乐东','乐东', 1);
INSERT INTO `dev_city` VALUES (156, 11,'临高','临高', 1);
INSERT INTO `dev_city` VALUES (157, 11,'陵水','陵水', 1);
INSERT INTO `dev_city` VALUES (158, 11,'南沙','南沙', 1);
INSERT INTO `dev_city` VALUES (159, 11,'琼海','琼海', 1);
INSERT INTO `dev_city` VALUES (160, 11,'琼中','琼中', 1);
INSERT INTO `dev_city` VALUES (161, 11,'三亚','三亚', 1);
INSERT INTO `dev_city` VALUES (162, 11,'屯昌','屯昌', 1);
INSERT INTO `dev_city` VALUES (163, 11,'万宁','万宁', 1);
INSERT INTO `dev_city` VALUES (164, 11,'文昌','文昌', 1);
INSERT INTO `dev_city` VALUES (165, 11,'五指山','五指山', 1);
INSERT INTO `dev_city` VALUES (166, 11,'西沙','西沙', 1);
INSERT INTO `dev_city` VALUES (167, 11,'中沙','中沙', 1);
INSERT INTO `dev_city` VALUES (168, 11,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (169, 12,'保定','保定', 1);
INSERT INTO `dev_city` VALUES (170, 12,'沧州','沧州', 1);
INSERT INTO `dev_city` VALUES (171, 12,'承德','承德', 1);
INSERT INTO `dev_city` VALUES (172, 12,'邯郸','邯郸', 1);
INSERT INTO `dev_city` VALUES (173, 12,'衡水','衡水', 1);
INSERT INTO `dev_city` VALUES (174, 12,'廊坊','廊坊', 1);
INSERT INTO `dev_city` VALUES (175, 12,'秦皇岛','秦皇岛', 1);
INSERT INTO `dev_city` VALUES (176, 12,'石家庄','石家庄', 1);
INSERT INTO `dev_city` VALUES (177, 12,'唐山','唐山', 1);
INSERT INTO `dev_city` VALUES (178, 12,'邢台','邢台', 1);
INSERT INTO `dev_city` VALUES (179, 12,'张家口','张家口', 1);
INSERT INTO `dev_city` VALUES (180, 12,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (181, 13,'安阳','安阳', 1);
INSERT INTO `dev_city` VALUES (182, 13,'鹤壁','鹤壁', 1);
INSERT INTO `dev_city` VALUES (183, 13,'焦作','焦作', 1);
INSERT INTO `dev_city` VALUES (184, 13,'开封','开封', 1);
INSERT INTO `dev_city` VALUES (185, 13,'洛阳','洛阳', 1);
INSERT INTO `dev_city` VALUES (186, 13,'漯河','漯河', 1);
INSERT INTO `dev_city` VALUES (187, 13,'南阳','南阳', 1);
INSERT INTO `dev_city` VALUES (188, 13,'平顶山','平顶山', 1);
INSERT INTO `dev_city` VALUES (189, 13,'濮阳','濮阳', 1);
INSERT INTO `dev_city` VALUES (190, 13,'三门峡','三门峡', 1);
INSERT INTO `dev_city` VALUES (191, 13,'商丘','商丘', 1);
INSERT INTO `dev_city` VALUES (192, 13,'新乡','新乡', 1);
INSERT INTO `dev_city` VALUES (193, 13,'信阳','信阳', 1);
INSERT INTO `dev_city` VALUES (194, 13,'许昌','许昌', 1);
INSERT INTO `dev_city` VALUES (195, 13,'郑州','郑州', 1);
INSERT INTO `dev_city` VALUES (196, 13,'周口','周口', 1);
INSERT INTO `dev_city` VALUES (197, 13,'驻马店','驻马店', 1);
INSERT INTO `dev_city` VALUES (198, 13,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (199, 14,'大庆','大庆', 1);
----------------------------------------------------------------------------------
INSERT INTO `dev_city` VALUES (200, 14,'大兴安岭','大兴安岭', 1);
INSERT INTO `dev_city` VALUES (201, 14,'哈尔滨','哈尔滨', 1);
INSERT INTO `dev_city` VALUES (202, 14,'鹤岗','鹤岗', 1);
INSERT INTO `dev_city` VALUES (203, 14,'黑河','黑河', 1);
INSERT INTO `dev_city` VALUES (204, 14,'鸡西','鸡西', 1);
INSERT INTO `dev_city` VALUES (205, 14,'佳木斯','佳木斯', 1);
INSERT INTO `dev_city` VALUES (206, 14,'牡丹江','牡丹江', 1);
INSERT INTO `dev_city` VALUES (207, 14,'七台河','七台河', 1);
INSERT INTO `dev_city` VALUES (208, 14,'齐齐哈尔','齐齐哈尔', 1);
INSERT INTO `dev_city` VALUES (209, 14,'双鸭山','双鸭山', 1);
INSERT INTO `dev_city` VALUES (210, 14,'绥化','绥化', 1);
INSERT INTO `dev_city` VALUES (211, 14,'伊春','伊春', 1);
INSERT INTO `dev_city` VALUES (212, 14,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (213, 15,'鄂州','鄂州', 1);
INSERT INTO `dev_city` VALUES (214, 15,'恩施','恩施', 1);
INSERT INTO `dev_city` VALUES (215, 15,'黄冈','黄冈', 1);
INSERT INTO `dev_city` VALUES (216, 15,'黄石','黄石', 1);
INSERT INTO `dev_city` VALUES (217, 15,'荆门','荆门', 1);
INSERT INTO `dev_city` VALUES (218, 15,'荆州','荆州', 1);
INSERT INTO `dev_city` VALUES (219, 15,'潜江','潜江', 1);
INSERT INTO `dev_city` VALUES (220, 15,'神农架','神农架', 1);
INSERT INTO `dev_city` VALUES (221, 15,'十堰','十堰', 1);
INSERT INTO `dev_city` VALUES (222, 15,'随州','随州', 1);
INSERT INTO `dev_city` VALUES (223, 15,'天门','天门', 1);
INSERT INTO `dev_city` VALUES (224, 15,'武汉','武汉', 1);
INSERT INTO `dev_city` VALUES (225, 15,'仙桃','仙桃', 1);
INSERT INTO `dev_city` VALUES (226, 15,'咸宁','咸宁', 1);
INSERT INTO `dev_city` VALUES (227, 15,'襄樊','襄樊', 1);
INSERT INTO `dev_city` VALUES (228, 15,'孝感','孝感', 1);
INSERT INTO `dev_city` VALUES (229, 15,'宜昌','宜昌', 1);
INSERT INTO `dev_city` VALUES (230, 15,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (231, 16,'长沙','长沙', 1);
INSERT INTO `dev_city` VALUES (232, 16,'常德','常德', 1);
INSERT INTO `dev_city` VALUES (233, 16,'郴州','郴州', 1);
INSERT INTO `dev_city` VALUES (234, 16,'衡阳','衡阳', 1);
INSERT INTO `dev_city` VALUES (235, 16,'怀化','怀化', 1);
INSERT INTO `dev_city` VALUES (236, 16,'娄底','娄底', 1);
INSERT INTO `dev_city` VALUES (237, 16,'邵阳','邵阳', 1);
INSERT INTO `dev_city` VALUES (238, 16,'湘潭','湘潭', 1);
INSERT INTO `dev_city` VALUES (239, 16,'湘西','湘西', 1);
INSERT INTO `dev_city` VALUES (240, 16,'益阳','益阳', 1);
INSERT INTO `dev_city` VALUES (241, 16,'永州','永州', 1);
INSERT INTO `dev_city` VALUES (242, 16,'岳阳','岳阳', 1);
INSERT INTO `dev_city` VALUES (243, 16,'张家界','张家界', 1);
INSERT INTO `dev_city` VALUES (244, 16,'株洲','株洲', 1);
INSERT INTO `dev_city` VALUES (245, 16,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (246, 17,'白城','白城', 1);
INSERT INTO `dev_city` VALUES (247, 17,'白山','白山', 1);
INSERT INTO `dev_city` VALUES (248, 17,'长春','长春', 1);
INSERT INTO `dev_city` VALUES (249, 17,'吉林','吉林', 1);
INSERT INTO `dev_city` VALUES (250, 17,'辽源','辽源', 1);
INSERT INTO `dev_city` VALUES (251, 17,'四平','四平', 1);
INSERT INTO `dev_city` VALUES (252, 17,'松原','松原', 1);
INSERT INTO `dev_city` VALUES (253, 17,'通化','通化', 1);
INSERT INTO `dev_city` VALUES (254, 17,'延边','延边', 1);
INSERT INTO `dev_city` VALUES (255, 17,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (256, 18,'常州','常州', 1);
INSERT INTO `dev_city` VALUES (257, 18,'淮安','淮安', 1);
INSERT INTO `dev_city` VALUES (258, 18,'连云港','连云港', 1);
INSERT INTO `dev_city` VALUES (259, 18,'南京','南京', 1);
INSERT INTO `dev_city` VALUES (260, 18,'南通','南通', 1);
INSERT INTO `dev_city` VALUES (261, 18,'苏州','苏州', 1);
INSERT INTO `dev_city` VALUES (262, 18,'宿迁','宿迁', 1);
INSERT INTO `dev_city` VALUES (263, 18,'泰州','泰州', 1);
INSERT INTO `dev_city` VALUES (264, 18,'无锡','无锡', 1);
INSERT INTO `dev_city` VALUES (265, 18,'徐州','徐州', 1);
INSERT INTO `dev_city` VALUES (266, 18,'盐城','盐城', 1);
INSERT INTO `dev_city` VALUES (267, 18,'扬州','扬州', 1);
INSERT INTO `dev_city` VALUES (268, 18,'镇江','镇江', 1);
INSERT INTO `dev_city` VALUES (269, 18,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (270, 19,'抚州','抚州', 1);
INSERT INTO `dev_city` VALUES (271, 19,'赣州','赣州', 1);
INSERT INTO `dev_city` VALUES (272, 19,'吉安','吉安', 1);
INSERT INTO `dev_city` VALUES (273, 19,'景德镇','景德镇', 1);
INSERT INTO `dev_city` VALUES (274, 19,'九江','九江', 1);
INSERT INTO `dev_city` VALUES (275, 19,'南昌','南昌', 1);
INSERT INTO `dev_city` VALUES (276, 19,'萍乡','萍乡', 1);
INSERT INTO `dev_city` VALUES (277, 19,'上饶','上饶', 1);
INSERT INTO `dev_city` VALUES (278, 19,'新余','新余', 1);
INSERT INTO `dev_city` VALUES (279, 19,'宜春','宜春', 1);
INSERT INTO `dev_city` VALUES (280, 19,'鹰潭','鹰潭', 1);
INSERT INTO `dev_city` VALUES (281, 19,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (282, 20,'鞍山','鞍山', 1);
INSERT INTO `dev_city` VALUES (283, 20,'本溪','本溪', 1);
INSERT INTO `dev_city` VALUES (284, 20,'朝阳','朝阳', 1);
INSERT INTO `dev_city` VALUES (285, 20,'大连','大连', 1);
INSERT INTO `dev_city` VALUES (286, 20,'丹东','丹东', 1);
INSERT INTO `dev_city` VALUES (287, 20,'抚顺','抚顺', 1);
INSERT INTO `dev_city` VALUES (288, 20,'阜新','阜新', 1);
INSERT INTO `dev_city` VALUES (289, 20,'葫芦岛','葫芦岛', 1);
INSERT INTO `dev_city` VALUES (290, 20,'锦州','锦州', 1);
INSERT INTO `dev_city` VALUES (291, 20,'辽阳','辽阳', 1);
INSERT INTO `dev_city` VALUES (292, 20,'盘锦','盘锦', 1);
INSERT INTO `dev_city` VALUES (293, 20,'沈阳','沈阳', 1);
INSERT INTO `dev_city` VALUES (294, 20,'铁岭','铁岭', 1);
INSERT INTO `dev_city` VALUES (295, 20,'营口','营口', 1);
INSERT INTO `dev_city` VALUES (296, 20,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (297, 21,'阿拉善','阿拉善', 1);
INSERT INTO `dev_city` VALUES (298, 21,'巴彦淖尔','巴彦淖尔', 1);
INSERT INTO `dev_city` VALUES (299, 21,'包头','包头', 1);
------------------------------------------------------------------------------------
INSERT INTO `dev_city` VALUES (300, 21,'赤峰','赤峰', 1);
INSERT INTO `dev_city` VALUES (301, 21,'鄂尔多斯','鄂尔多斯', 1);
INSERT INTO `dev_city` VALUES (302, 21,'呼和浩特','呼和浩特', 1);
INSERT INTO `dev_city` VALUES (303, 21,'呼伦贝尔','呼伦贝尔', 1);
INSERT INTO `dev_city` VALUES (304, 21,'通辽','通辽', 1);
INSERT INTO `dev_city` VALUES (305, 21,'乌海','乌海', 1);
INSERT INTO `dev_city` VALUES (306, 21,'乌兰察布','乌兰察布', 1);
INSERT INTO `dev_city` VALUES (307, 21,'锡林郭勒','锡林郭勒', 1);
INSERT INTO `dev_city` VALUES (308, 21,'兴安','兴安', 1);
INSERT INTO `dev_city` VALUES (309, 21,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (310, 22,'固原','固原', 1);
INSERT INTO `dev_city` VALUES (311, 22,'石嘴山','石嘴山', 1);
INSERT INTO `dev_city` VALUES (312, 22,'吴忠','吴忠', 1);
INSERT INTO `dev_city` VALUES (313, 22,'银川','银川', 1);
INSERT INTO `dev_city` VALUES (314, 22,'中卫','中卫', 1);
INSERT INTO `dev_city` VALUES (315, 22,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (316, 23,'果洛','果洛', 1);
INSERT INTO `dev_city` VALUES (317, 23,'海北','海北', 1);
INSERT INTO `dev_city` VALUES (318, 23,'海东','海东', 1);
INSERT INTO `dev_city` VALUES (319, 23,'海南','海南', 1);
INSERT INTO `dev_city` VALUES (320, 23,'海西','海西', 1);
INSERT INTO `dev_city` VALUES (321, 23,'黄南','黄南', 1);
INSERT INTO `dev_city` VALUES (322, 23,'西宁','西宁', 1);
INSERT INTO `dev_city` VALUES (323, 23,'玉树','玉树', 1);
INSERT INTO `dev_city` VALUES (324, 23,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (325, 24,'滨州','滨州', 1);
INSERT INTO `dev_city` VALUES (326, 24,'德州','德州', 1);
INSERT INTO `dev_city` VALUES (327, 24,'东营','东营', 1);
INSERT INTO `dev_city` VALUES (328, 24,'菏泽','菏泽', 1);
INSERT INTO `dev_city` VALUES (329, 24,'济南','济南', 1);
INSERT INTO `dev_city` VALUES (330, 24,'济宁','济宁', 1);
INSERT INTO `dev_city` VALUES (331, 24,'莱芜','莱芜', 1);
INSERT INTO `dev_city` VALUES (332, 24,'聊城','聊城', 1);
INSERT INTO `dev_city` VALUES (333, 24,'临沂','临沂', 1);
INSERT INTO `dev_city` VALUES (334, 24,'青岛','青岛', 1);
INSERT INTO `dev_city` VALUES (335, 24,'日照','日照', 1);
INSERT INTO `dev_city` VALUES (336, 24,'泰安','泰安', 1);
INSERT INTO `dev_city` VALUES (337, 24,'威海','威海', 1);
INSERT INTO `dev_city` VALUES (338, 24,'潍坊','潍坊', 1);
INSERT INTO `dev_city` VALUES (339, 24,'烟台','烟台', 1);
INSERT INTO `dev_city` VALUES (340, 24,'枣庄','枣庄', 1);
INSERT INTO `dev_city` VALUES (341, 24,'淄博','淄博', 1);
INSERT INTO `dev_city` VALUES (342, 24,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (343, 25,'长治','长治', 1);
INSERT INTO `dev_city` VALUES (344, 25,'大同','大同', 1);
INSERT INTO `dev_city` VALUES (345, 25,'晋城','晋城', 1);
INSERT INTO `dev_city` VALUES (346, 25,'晋中','晋中', 1);
INSERT INTO `dev_city` VALUES (347, 25,'临汾','临汾', 1);
INSERT INTO `dev_city` VALUES (348, 25,'吕梁','吕梁', 1);
INSERT INTO `dev_city` VALUES (349, 25,'朔州','朔州', 1);
INSERT INTO `dev_city` VALUES (350, 25,'太原','太原', 1);
INSERT INTO `dev_city` VALUES (351, 25,'忻州','忻州', 1);
INSERT INTO `dev_city` VALUES (352, 25,'阳泉','阳泉', 1);
INSERT INTO `dev_city` VALUES (353, 25,'运城','运城', 1);
INSERT INTO `dev_city` VALUES (354, 25,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (355, 26,'安康','安康', 1);
INSERT INTO `dev_city` VALUES (356, 26,'宝鸡','宝鸡', 1);
INSERT INTO `dev_city` VALUES (357, 26,'汉中','汉中', 1);
INSERT INTO `dev_city` VALUES (358, 26,'商洛','商洛', 1);
INSERT INTO `dev_city` VALUES (359, 26,'铜川','铜川', 1);
INSERT INTO `dev_city` VALUES (360, 26,'渭南','渭南', 1);
INSERT INTO `dev_city` VALUES (361, 26,'西安','西安', 1);
INSERT INTO `dev_city` VALUES (362, 26,'咸阳','咸阳', 1);
INSERT INTO `dev_city` VALUES (363, 26,'延安','延安', 1);
INSERT INTO `dev_city` VALUES (364, 26,'榆林','榆林', 1);
INSERT INTO `dev_city` VALUES (365, 26,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (366, 27,'阿坝','阿坝', 1);
INSERT INTO `dev_city` VALUES (367, 27,'巴中','巴中', 1);
INSERT INTO `dev_city` VALUES (368, 27,'成都','成都', 1);
INSERT INTO `dev_city` VALUES (369, 27,'达州','达州', 1);
INSERT INTO `dev_city` VALUES (370, 27,'德阳','德阳', 1);
INSERT INTO `dev_city` VALUES (371, 27,'甘孜','甘孜', 1);
INSERT INTO `dev_city` VALUES (372, 27,'广安','广安', 1);
INSERT INTO `dev_city` VALUES (373, 27,'广元','广元', 1);
INSERT INTO `dev_city` VALUES (374, 27,'乐山','乐山', 1);
INSERT INTO `dev_city` VALUES (375, 27,'凉山','凉山', 1);
INSERT INTO `dev_city` VALUES (376, 27,'泸州','泸州', 1);
INSERT INTO `dev_city` VALUES (377, 27,'眉山','眉山', 1);
INSERT INTO `dev_city` VALUES (378, 27,'绵阳','绵阳', 1);
INSERT INTO `dev_city` VALUES (379, 27,'内江','内江', 1);
INSERT INTO `dev_city` VALUES (380, 27,'南充','南充', 1);
INSERT INTO `dev_city` VALUES (381, 27,'攀枝花','攀枝花', 1);
INSERT INTO `dev_city` VALUES (382, 27,'遂宁','遂宁', 1);
INSERT INTO `dev_city` VALUES (383, 27,'雅安','雅安', 1);
INSERT INTO `dev_city` VALUES (384, 27,'宜宾','宜宾', 1);
INSERT INTO `dev_city` VALUES (385, 27,'资阳','资阳', 1);
INSERT INTO `dev_city` VALUES (386, 27,'自贡','自贡', 1);
INSERT INTO `dev_city` VALUES (387, 27,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (388, 28,'阿里','阿里', 1);
INSERT INTO `dev_city` VALUES (389, 28,'昌都','昌都', 1);
INSERT INTO `dev_city` VALUES (390, 28,'拉萨','拉萨', 1);
INSERT INTO `dev_city` VALUES (391, 28,'林芝','林芝', 1);
INSERT INTO `dev_city` VALUES (392, 28,'那曲','那曲', 1);
INSERT INTO `dev_city` VALUES (393, 28,'日喀则','日喀则', 1);
INSERT INTO `dev_city` VALUES (394, 28,'山南','山南', 1);
INSERT INTO `dev_city` VALUES (395, 28,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (396, 29,'阿克苏','阿克苏', 1);
INSERT INTO `dev_city` VALUES (397, 29,'阿拉尔','阿拉尔', 1);
INSERT INTO `dev_city` VALUES (398, 29,'阿勒泰','阿勒泰', 1);
INSERT INTO `dev_city` VALUES (399, 29,'巴音郭楞','巴音郭楞', 1);
------------------------------------------------------------------------------
INSERT INTO `dev_city` VALUES (400, 29,'博尔塔拉','博尔塔拉', 1);
INSERT INTO `dev_city` VALUES (401, 29,'昌吉','昌吉', 1);
INSERT INTO `dev_city` VALUES (402, 29,'哈密','哈密', 1);
INSERT INTO `dev_city` VALUES (403, 29,'和田','和田', 1);
INSERT INTO `dev_city` VALUES (404, 29,'喀什','喀什', 1);
INSERT INTO `dev_city` VALUES (405, 29,'克拉玛依','克拉玛依', 1);
INSERT INTO `dev_city` VALUES (406, 29,'克孜勒苏','克孜勒苏', 1);
INSERT INTO `dev_city` VALUES (407, 29,'石河子','石河子', 1);
INSERT INTO `dev_city` VALUES (408, 29,'塔城','塔城', 1);
INSERT INTO `dev_city` VALUES (409, 29,'图木舒克','图木舒克', 1);
INSERT INTO `dev_city` VALUES (410, 29,'吐鲁番','吐鲁番', 1);
INSERT INTO `dev_city` VALUES (411, 29,'乌鲁木齐','乌鲁木齐', 1);
INSERT INTO `dev_city` VALUES (412, 29,'五家渠','五家渠', 1);
INSERT INTO `dev_city` VALUES (413, 29,'伊犁','伊犁', 1);
INSERT INTO `dev_city` VALUES (414, 29,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (415, 30,'保山','保山', 1);
INSERT INTO `dev_city` VALUES (416, 30,'楚雄','楚雄', 1);
INSERT INTO `dev_city` VALUES (417, 30,'大理','大理', 1);
INSERT INTO `dev_city` VALUES (418, 30,'德宏','德宏', 1);
INSERT INTO `dev_city` VALUES (419, 30,'迪庆','迪庆', 1);
INSERT INTO `dev_city` VALUES (420, 30,'红河','红河', 1);
INSERT INTO `dev_city` VALUES (421, 30,'昆明','昆明', 1);
INSERT INTO `dev_city` VALUES (422, 30,'丽江','丽江', 1);
INSERT INTO `dev_city` VALUES (423, 30,'临沧','临沧', 1);
INSERT INTO `dev_city` VALUES (424, 30,'怒江','怒江', 1);
INSERT INTO `dev_city` VALUES (425, 30,'曲靖','曲靖', 1);
INSERT INTO `dev_city` VALUES (426, 30,'思茅','思茅', 1);
INSERT INTO `dev_city` VALUES (427, 30,'文山','文山', 1);
INSERT INTO `dev_city` VALUES (428, 30,'西双版纳','西双版纳', 1);
INSERT INTO `dev_city` VALUES (429, 30,'玉溪','玉溪', 1);
INSERT INTO `dev_city` VALUES (430, 30,'昭通','昭通', 1);
INSERT INTO `dev_city` VALUES (431, 30,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (432, 31,'杭州','杭州', 1);
INSERT INTO `dev_city` VALUES (433, 31,'湖州','湖州', 1);
INSERT INTO `dev_city` VALUES (434, 31,'嘉兴','嘉兴', 1);
INSERT INTO `dev_city` VALUES (435, 31,'金华','金华', 1);
INSERT INTO `dev_city` VALUES (436, 31,'丽水','丽水', 1);
INSERT INTO `dev_city` VALUES (437, 31,'宁波','宁波', 1);
INSERT INTO `dev_city` VALUES (438, 31,'衢州','衢州', 1);
INSERT INTO `dev_city` VALUES (439, 31,'绍兴','绍兴', 1);
INSERT INTO `dev_city` VALUES (440, 31,'台州','台州', 1);
INSERT INTO `dev_city` VALUES (441, 31,'温州','温州', 1);
INSERT INTO `dev_city` VALUES (442, 31,'舟山','舟山', 1);
INSERT INTO `dev_city` VALUES (443, 31,'其他','其他', 1);
INSERT INTO `dev_city` VALUES (444, 32,'香港特别行政区','香港特别行政区', 1);
INSERT INTO `dev_city` VALUES (445, 33,'澳门特别行政区','澳门特别行政区', 1);
INSERT INTO `dev_city` VALUES (446, 34,'高雄市','高雄市', 1);
INSERT INTO `dev_city` VALUES (447, 34,'高雄县','高雄县', 1);
INSERT INTO `dev_city` VALUES (448, 34,'花莲县','花莲县', 1);
INSERT INTO `dev_city` VALUES (449, 34,'基隆市','基隆市', 1);
INSERT INTO `dev_city` VALUES (450, 34,'嘉义市','嘉义市', 1);
INSERT INTO `dev_city` VALUES (451, 34,'嘉义县','嘉义县', 1);
INSERT INTO `dev_city` VALUES (452, 34,'苗栗县','苗栗县', 1);
INSERT INTO `dev_city` VALUES (453, 34,'南投县','南投县', 1);
INSERT INTO `dev_city` VALUES (454, 34,'澎湖县','澎湖县', 1);
INSERT INTO `dev_city` VALUES (455, 34,'屏东县','屏东县', 1);
INSERT INTO `dev_city` VALUES (456, 34,'台北市','台北市', 1);
INSERT INTO `dev_city` VALUES (457, 34,'台北县','台北县', 1);
INSERT INTO `dev_city` VALUES (458, 34,'台东县','台东县', 1);
INSERT INTO `dev_city` VALUES (459, 34,'台南市','台南市', 1);
INSERT INTO `dev_city` VALUES (460, 34,'台南县','台南县', 1);
INSERT INTO `dev_city` VALUES (461, 34,'台中市','台中市', 1);
INSERT INTO `dev_city` VALUES (462, 34,'台中县','台中县', 1);
INSERT INTO `dev_city` VALUES (463, 34,'桃园县','桃园县', 1);
INSERT INTO `dev_city` VALUES (464, 34,'新竹市','新竹市', 1);
INSERT INTO `dev_city` VALUES (465, 34,'新竹县','新竹县', 1);
INSERT INTO `dev_city` VALUES (466, 34,'宜兰县','宜兰县', 1);
INSERT INTO `dev_city` VALUES (467, 34,'云林县','云林县', 1);
INSERT INTO `dev_city` VALUES (468, 34,'彰化县','彰化县', 1);
INSERT INTO `dev_city` VALUES (469, 34,'其他','其他', 1);
SET FOREIGN_KEY_CHECKS = 1;





