const { videoUpload } = require("../middlewares/videoUpload");
const { addVideo, getAllVideos } = require("../controllers/video");

const router = require("express").Router();

router.post("/upload", videoUpload.single("video"), addVideo);

router.get("/videos", getAllVideos);

module.exports = router;
