SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for table_代理日志管理等级1
-- ----------------------------
DROP TABLE IF EXISTS `table_代理日志管理等级1`;
CREATE TABLE `table_代理日志管理等级1`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `代理ID` int(32) NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga验证` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IP` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作人` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间操作` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_代理日志管理等级2
-- ----------------------------
DROP TABLE IF EXISTS `table_代理日志管理等级2`;
CREATE TABLE `table_代理日志管理等级2`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `代理ID` int(32) NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga验证` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IP` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作人` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间操作` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_代理账号等级1
-- ----------------------------
DROP TABLE IF EXISTS `table_代理账号等级1`;
CREATE TABLE `table_代理账号等级1`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `代理ID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `代理密码` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `代理名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `绑定邮箱` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `绑定手机` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `登入错误累计` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间最后登入` datetime(0) NULL DEFAULT NULL,
  `时间注册` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_代理账号等级2
-- ----------------------------
DROP TABLE IF EXISTS `table_代理账号等级2`;
CREATE TABLE `table_代理账号等级2`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `代理ID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `代理密码` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `代理名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `绑定邮箱` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `绑定手机` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `登入错误累计` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间最后登入` datetime(0) NULL DEFAULT NULL,
  `时间注册` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `所属代理L1` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_后台出款银行卡流水
-- ----------------------------
DROP TABLE IF EXISTS `table_后台出款银行卡流水`;
CREATE TABLE `table_后台出款银行卡流水`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `订单号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收入` double(32, 2) NULL DEFAULT NULL,
  `支出` double(32, 2) NULL DEFAULT NULL,
  `余额` double(32, 2) NULL DEFAULT NULL,
  `商户ID` int(16) NULL DEFAULT NULL,
  `出款银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `备注` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `类型` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT NULL,
  `时间交易` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_后台出款银行卡管理
-- ----------------------------
DROP TABLE IF EXISTS `table_后台出款银行卡管理`;
CREATE TABLE `table_后台出款银行卡管理`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡余额` double(32, 2) NULL DEFAULT NULL,
  `出款银行卡主姓名` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡主电话` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡每日限额` double(32, 2) NULL DEFAULT NULL,
  `出款银行卡最小交易金额` double(32, 2) NULL DEFAULT NULL,
  `显示标记` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_后台收款银行卡流水
-- ----------------------------
DROP TABLE IF EXISTS `table_后台收款银行卡流水`;
CREATE TABLE `table_后台收款银行卡流水`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `订单号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收入` double(32, 2) NULL DEFAULT NULL,
  `支出` double(32, 2) NULL DEFAULT NULL,
  `余额` double(32, 2) NULL DEFAULT NULL,
  `商户ID` int(16) NULL DEFAULT NULL,
  `收款银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收款银行卡名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `备注` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `类型` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT NULL,
  `时间交易` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_后台收款银行卡管理
-- ----------------------------
DROP TABLE IF EXISTS `table_后台收款银行卡管理`;
CREATE TABLE `table_后台收款银行卡管理`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收款银行卡名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收款银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收款银行名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收款银行卡余额` double(32, 2) NULL DEFAULT NULL,
  `收款银行卡主姓名` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收款银行卡主电话` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `显示标记` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_后台日志管理
-- ----------------------------
DROP TABLE IF EXISTS `table_后台日志管理`;
CREATE TABLE `table_后台日志管理`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `后台ID` int(32) NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga验证` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IP` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作人` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间操作` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_后台白名单管理
-- ----------------------------
DROP TABLE IF EXISTS `table_后台白名单管理`;
CREATE TABLE `table_后台白名单管理`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `后台ID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `后台白名单IP` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `后台白名单备注` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of table_后台白名单管理
-- ----------------------------
INSERT INTO `table_后台白名单管理` VALUES (1, '111111111111', '123456', '::1', NULL, '启用', '2019-02-10 15:53:24');

-- ----------------------------
-- Table structure for table_后台账号
-- ----------------------------
DROP TABLE IF EXISTS `table_后台账号`;
CREATE TABLE `table_后台账号`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `后台ID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `后台密码` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `后台账号名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `后台账号分级` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间最后登入` datetime(0) NULL DEFAULT NULL,
  `时间注册` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of table_后台账号
-- ----------------------------
INSERT INTO `table_后台账号` VALUES (1, '123456', 'admin', '5d8aa958f6', '名称', 'L1', '启用', '2020-02-10 15:56:01', '2019-01-29 21:25:51');

-- ----------------------------
-- Table structure for table_商户平台公告
-- ----------------------------
DROP TABLE IF EXISTS `table_商户平台公告`;
CREATE TABLE `table_商户平台公告`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `标题` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间` datetime(0) NULL DEFAULT NULL,
  `内容` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_商户日志管理
-- ----------------------------
DROP TABLE IF EXISTS `table_商户日志管理`;
CREATE TABLE `table_商户日志管理`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户ID` int(32) NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga验证` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IP` varchar(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作人` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间操作` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of table_商户日志管理
-- ----------------------------
INSERT INTO `table_商户日志管理` VALUES (1, 'MLL202002101550249493', 123, '5d8aa958f6', '659899', '::1', NULL, '登入商户账户', '2020-02-10 15:50:24');

-- ----------------------------
-- Table structure for table_商户明细余额
-- ----------------------------
DROP TABLE IF EXISTS `table_商户明细余额`;
CREATE TABLE `table_商户明细余额`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `订单号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户ID` int(32) NULL DEFAULT NULL,
  `手续费` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易金额` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易前账户余额` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易后账户余额` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `类型` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_商户明细充值
-- ----------------------------
DROP TABLE IF EXISTS `table_商户明细充值`;
CREATE TABLE `table_商户明细充值`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `订单号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户ID` int(32) NULL DEFAULT NULL,
  `商户银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `收款银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `充值类型` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `充值金额` double(32, 2) NULL DEFAULT 0.00,
  `产生手续费` double(32, 2) NULL DEFAULT NULL,
  `备注商户` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `备注后台` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `商户充值目标姓名` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户充值目标卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户充值目标银行` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_商户明细手续费
-- ----------------------------
DROP TABLE IF EXISTS `table_商户明细手续费`;
CREATE TABLE `table_商户明细手续费`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `订单号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户ID` int(32) NULL DEFAULT NULL,
  `收款` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易金额` double(32, 2) NULL DEFAULT NULL,
  `手续费收入` double(32, 2) NULL DEFAULT NULL,
  `手续费支出` double(32, 2) NULL DEFAULT NULL,
  `交易前手续费余额` double(32, 2) NULL DEFAULT NULL,
  `交易后手续费余额` double(32, 2) NULL DEFAULT NULL,
  `备注` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `类型` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_商户明细提款
-- ----------------------------
DROP TABLE IF EXISTS `table_商户明细提款`;
CREATE TABLE `table_商户明细提款`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `订单号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户API订单号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户ID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `出款银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易方卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易方姓名` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易方银行` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `交易金额` double(32, 2) NULL DEFAULT NULL,
  `手续费` double(32, 2) NULL DEFAULT NULL,
  `备注商户写` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `备注管理写` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `创建方式` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '下单方式',
  `类型` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `时间完成` datetime(0) NULL DEFAULT NULL,
  `时间修改` datetime(0) NULL DEFAULT NULL,
  `订单源IP` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `操作员` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `时间创建`(`时间创建`) USING BTREE,
  INDEX `状态_时间创建`(`状态`, `时间创建`) USING BTREE,
  INDEX `商户_时间`(`商户ID`, `时间创建`) USING BTREE,
  INDEX `商户_状态_时间`(`商户ID`, `状态`, `时间创建`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for table_商户白名单管理
-- ----------------------------
DROP TABLE IF EXISTS `table_商户白名单管理`;
CREATE TABLE `table_商户白名单管理`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户ID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户白名单IP` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户白名单备注` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of table_商户白名单管理
-- ----------------------------
INSERT INTO `table_商户白名单管理` VALUES (1, '11111111111', '123', '::1', NULL, '启用', '2019-02-10 15:49:46');

-- ----------------------------
-- Table structure for table_商户账号
-- ----------------------------
DROP TABLE IF EXISTS `table_商户账号`;
CREATE TABLE `table_商户账号`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `商户ID` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户密码` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户密码API` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `keyga` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `支付密码` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `绑定邮箱` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `绑定手机` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `登入错误累计` int(6) NULL DEFAULT NULL,
  `支付错误累计` int(6) NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间最后登入` datetime(0) NULL DEFAULT NULL,
  `时间注册` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `所属管理L2` varchar(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `所属代理L1` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `所属代理L2` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `提款余额` double(32, 2) NULL DEFAULT NULL,
  `手续费余额` double(32, 2) NULL DEFAULT NULL,
  `手续费收款方式` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `手续费比率` double(32, 2) NULL DEFAULT NULL,
  `单笔手续费` double(32, 2) NULL DEFAULT NULL,
  `充值最低手续费` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `充值最低余额` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `提款最低单笔金额` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `提款最高单笔金额` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第一阶梯起` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第一阶梯止` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第一阶梯百分比` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第二阶梯起` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第二阶梯止` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第二阶梯百分比` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第三阶梯起` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第三阶梯止` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第三阶梯百分比` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第四阶梯起` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第四阶梯止` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `第四阶梯百分比` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of table_商户账号
-- ----------------------------
INSERT INTO `table_商户账号` VALUES (1, '123', '456', 'qweasdzxc', '5d8aa958f6', '789', '测试号', '54217859123@qq.com', '1', 2, 0, '启用', '2020-02-10 15:50:24', '2019-02-10 15:45:12', '启用', '停用', '启用', 923.00, 9.80, '0.1', 0.10, 0.10, '0.00', '0.00', '1', '1000', '0.00', '1', '1', '1', '50000', '0.5', '0.5', '0.5', '0.5', '0.5', '0.5', '0.5');

-- ----------------------------
-- Table structure for table_商户银行卡
-- ----------------------------
DROP TABLE IF EXISTS `table_商户银行卡`;
CREATE TABLE `table_商户银行卡`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `编号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户ID` int(32) NULL DEFAULT NULL,
  `商户银行卡名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户银行卡卡号` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户银行名称` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `商户银行卡主姓名` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `状态` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `时间创建` datetime(0) NULL DEFAULT CURRENT_TIMESTAMP(0),
  `商户银行卡卡标记` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
