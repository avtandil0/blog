<template>
  <v-container>
    <v-row align="center" justify="center" v-for="video in videos" :key="video.key">
      <v-card elevation="0" class="mx-auto" style="width:650px;">
        <youtube :video-id="$youtube.getIdFromURL(video.id)"></youtube>
        <v-card-title>Top western road trips</v-card-title>

        <v-card-subtitle >1,000 miles of wondemiles of wondemiles of wondemiles of wondemiles of wonde
          miles of wondemiles of wondemiles of wondemiles of wonde
          miles of wondemiles of wondemiles of wonder</v-card-subtitle>
        <hr />
        <br />
      </v-card>
    </v-row>
    <!-- <v-row v-if="grid">
      <v-col cols="12" md="6" v-for="video in videos" :key="video.key">
        <youtube :video-id="$youtube.getIdFromURL(video.id)"></youtube>
      </v-col>
    </v-row>-->

    <!-- <div class="text-center">
      <v-pagination v-model="page" :length="6"></v-pagination>
    </div>-->
  </v-container>
</template>

<script>
export default {
  name: "Videos",

  data: () => ({
    show: false,
    videoId: null,
    videos: []
  }),
  computed: {
    grid() {
      console.log("this.$store.state.grid", typeof this.$store.state.grid);
      return this.$store.state.grid;
    }
  },
  created() {
    scroll(0, 0);
    this.videos.push(
      {
        id: "https://www.youtube.com/watch?v=aDGcyKhPV0M",
        key: 1
      },
      {
        id: "https://www.youtube.com/watch?v=aDGcyKhPV0M",
        key: 2
      },
      {
        id: "https://www.youtube.com/watch?v=aDGcyKhPV0M",
        key: 3
      },
      {
        id: "https://www.youtube.com/watch?v=aDGcyKhPV0M",
        key: 4
      },
      {
        id: "https://www.youtube.com/watch?v=aDGcyKhPV0M",
        key: 5
      }
    );
  }
};
</script>
