<template>
  <v-container>
    <v-row align="center" justify="center" v-for="video in videos" :key="video.key">
      <v-card elevation="0" class="mx-auto" style="width:650px;">
        <youtube :video-id="$youtube.getIdFromURL(video.url)"></youtube>
        <v-card-title>{{video.title}}</v-card-title>

        <v-card-subtitle>{{video.description}}</v-card-subtitle>

        <v-card-actions>
          <v-btn
            @click="editVideo(video.id)"
            v-if="currentUser"
            class="ma-2"
            outlined
            color="indigo"
          >
            <v-icon>mdi-pencil</v-icon>
          </v-btn>
          <v-btn @click="openDeleteArticleDialog(video.id)" v-if="currentUser" class="ma-2" outlined color="red">
            <v-icon>mdi-delete</v-icon>
          </v-btn>
          <v-spacer></v-spacer>
          <div class="overline mb-4">{{video.dateCreated | dateToDayMonthYear}}</div>
        </v-card-actions>
        <hr />
        <br />
      </v-card>
    </v-row>
    <div class="text-center">
      <v-pagination
        :length="pagination.length"
        v-model="pagination.page"
        :total-visible="pagination.totalVisible"
        @input="onPaginationChange"
        :disabled="!isReady"
      />
    </div>

    <v-dialog v-model="deleteDialog" max-width="290">
      <v-card>
        <v-card-title class="headline red lighten-2">წაშლა</v-card-title>
        <br />
        <v-card-text>
          <h3>გსურთ ვიდეოს წაშლა?</h3>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="deleteDialog = false">არა</v-btn>

          <v-btn color="red darken-1" text @click="deleteVideo">დიახ</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
export default {
  name: "Videos",

  data: () => ({
    deleteDialog: false,
    deleteId: null,
    show: false,
    videoId: null,
    videos: [],
    isReady: false,
    page: 1,
    pagination: {
      totalVisible: 7,
      length: 20,
      page: 1,
      pageSize: 5
    },
    patientsPerPageOptions: [5, 10, 20, 50, 100],
    recordsTotalCount: null,
    currentUser: null
  }),
  computed: {
    grid() {
      console.log("this.$store.state.grid", typeof this.$store.state.grid);
      return this.$store.state.grid;
    }
  },
  created() {
    scroll(0, 0);
    this.currentUser = localStorage.getItem("currentUser");
    this.search();
  },
  methods: {
    editVideo(id) {
      console.log("id", id);
      this.$router.push({ name: "EditVideo", params: { id: id } });
    },
    async deleteVideo() {
      console.log("bbbb");
      console.log("delete");
      var result = await this.$http.deleteVideo(this.deleteId);

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

      if (result.isSuccess) {
        this.deleteDialog = false;
        this.deleteId = null;
        this.search();
        return;
      }


    },
    async openDeleteArticleDialog(id) {
      console.log("delete");
      this.deleteDialog = true;
      this.deleteId = id;

    },
    async search() {
      this.$store.commit("loadData", true);

      const result = await this.$http.getVideos({
        pageIndex: 1,
        pageSize: this.pagination.pageSize
      });

      this.videos = result.items;
      console.log("this.videos", this.videos);
      this.recordsTotalCount = result.itemsCount;

      this.pagination.length = result.pagesCount;
      this.pagination.page = result.currentIndex;
      this.isReady = true;
      // this.orkrgList = this.orkrgList.filter(r =>  r.ordernr.includes(this.searchForm.ordernr))
      console.log("this.orkrgList", this.orkrgList);
      this.$store.commit("loadData", false);
    },
    async onPaginationChange(index) {
      this.isReady = false;
      this.$store.commit("loadData", true);

      const result = await this.$http.getVideos({
        pageIndex: index,
        pageSize: this.pagination.pageSize
      });
      // this.statementList = [];

      this.videos = result.items;

      this.isReady = true;
      this.$store.commit("loadData", false);
      scroll(0, 0);
    }
  }
};
</script>
