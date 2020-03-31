import React from "react";
import { Card, CardImg, CardBody, CardTitle } from "reactstrap";

const ListNews2 = () => {
  return (
    <div className="slide-container">
      <Card>
        <CardImg
          top
          width="100%"
          src="https://due.udn.vn/Portals/0/Editor/TruyenThong_DUE/Nam%202019%202/Quy%204/SV%20Giai%201%20Eureka%202019/GiaiNhatEureka1.jpg"
          alt="Card image cap"
        />
        <CardBody>
          <CardTitle>
            <a href="https://due.udn.vn/vi-vn/nghiencuukhoahoc/nghiencuukhoahocchitiet/id/10838/cid/186">
              Sinh viên Kinh tế giành giải nhất giải thưởng nghiên cứu khoa học
              EURÉKA lần thứ 21- Năm 2019
            </a>
          </CardTitle>
        </CardBody>
      </Card>
    </div>
  );
};

export default ListNews2;
