import React, { Component } from "react";
import { Zoom } from "react-slideshow-image";
import "../../Dashboard/dashboard.css";

const images = [
  "https://scontent-hkg3-1.xx.fbcdn.net/v/t1.0-9/78038434_1760512070749488_9128787880515207168_o.jpg?_nc_cat=100&_nc_sid=8024bb&_nc_ohc=nL_mZDKG_XEAX9BO-nF&_nc_ht=scontent-hkg3-1.xx&oh=766f4fe9030518a996e576e80c49545a&oe=5E9AF405",
  "https://due.udn.vn/Portals/0/Editor/TruyenThong_DUE/Nam%202019%202/Quy%204/Giai%203%20NCKH%20TP/Giai3NCKHcapTP2019.jpg"
];
const zoomOutProperties = {
  duration: 5000,
  transitionDuration: 500,
  infinite: true,
  indicators: true,
  scale: 0.4,
  arrows: true
};

class Home extends Component {
  render() {
    return (
      <div className="slide-container">
        <Zoom {...zoomOutProperties} style={{ width: "140%" }}>
          {images.map((each, index) => (
            <img key={index} style={{ width: "100%" }} src={each} />
          ))}
        </Zoom>
        {/* <a href="https://due.udn.vn/vi-vn/nghiencuukhoahoc">
          <h3 style={{ marginRight: "0%" }}>
            Một số hoạt động nghiên cứu khoa học của giảng viên, sinh viên đại
            học Kinh tế
          </h3>
        </a> */}
      </div>
    );
  }
}
export default Home;
