import React, { Component } from "react";
import "../../Dashboard/dashboard.css";
import { List, Card, Avatar } from "antd";
import { MessageOutlined, LikeOutlined, StarOutlined } from "@ant-design/icons";

const listData = [
  {
    href: "https://due.udn.vn/vi-vn/tintuc/tintucchitiet/id/10756",
    title: `Sinh viên Kinh tế giành giải nhất giải thưởng Nghiên cứu khoa học EURÉKA lần thứ 21 - Năm 2019`
  }
];

class ListNews extends Component {
  render() {
    return (
      <List
        style={{ marginLeft: "0%", listStyleType: "" }}
        dataSource={listData}
        renderItem={item => (
          <List.Item
            key={item.title}
            extra={
              <img
                width={272}
                alt="logo"
                src="https://due.udn.vn/Portals/0/Editor/TruyenThong_DUE/Nam%202019%202/Quy%204/SV%20Giai%201%20Eureka%202019/GiaiNhatEureka1.jpg"
              />
            }
          >
            <List.Item.Meta title={<a href={item.href}>{item.title}</a>} />
          </List.Item>
        )}
      />
    );
  }
}

export default ListNews;
