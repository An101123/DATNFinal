import React, { Component } from "react";
import { List, Card, Col } from "antd";

const data = [
  {
    title:
      "Sinh viên Kinh tế giành giải nhất Nghiên cứu khoa học EURÉKA lần thứ 21",
    image:
      "https://due.udn.vn/Portals/0/Editor/TruyenThong_DUE/Nam%202019%202/Quy%204/SV%20Giai%201%20Eureka%202019/GiaiNhatEureka1.jpg",
    href:
      "https://due.udn.vn/vi-vn/nghiencuukhoahoc/nghiencuukhoahocchitiet/id/10756/cid/186"
  },
  {
    title:
      "Đề tài nghiên cứu khoa học của giảng viên đại học Đà Nẵng được ứng dụng thực tiễn",
    image:
      "http://www.udn.vn/app/webroot/upload/images/images1551065_4_chuy_n___i_n_ng_l__ng.jpg",
    href:
      "https://due.udn.vn/vi-vn/nghiencuukhoahoc/nghiencuukhoahocchitiet/id/10838/cid/186"
  }
];
class ListNews2 extends Component {
  render() {
    return (
      <div>
        <Col>
          <Card>
            {" "}
            {/* {images.map((each, index) => (
            <img
              key={index}
              style={{ width: "80%", marginLeft: "40px" }}
              src={each}
            />
          ))}
          ; */}
            {data.map((item, index) => (
              <div key={index}>
                <img
                  key={index}
                  style={{ width: "80%", marginLeft: "40px", marginTop: "6px" }}
                  src={item.image}
                />
                <a href={item.href}>
                  <h4
                    style={{
                      marginLeft: "40px",
                      marginRight: "40px"
                    }}
                  >
                    {item.title}
                  </h4>
                </a>{" "}
              </div>
            ))}
          </Card>
        </Col>
      </div>
    );
  }
}

export default ListNews2;
