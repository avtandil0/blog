<template>
  <v-container>
    <!-- <div v-if="!isReady">
      <div v-for="n in 5" :key="n">
        <v-skeleton-loader :elevation="1" class="mx-auto" max-width="800" type="card"></v-skeleton-loader>
        <v-skeleton-loader :elevation="1" class="mx-auto" max-width="800" type="list-item-two-line"></v-skeleton-loader>
        <br />
      </div>
    </div> -->
    <div>
      <div v-if="!grid ">
        <div v-for="article in articles" :key="article.id">
          <Card :article="article" />
        </div>
      </div>
      <v-row v-if="grid">
        <v-col cols="12" md="4" v-for="article in articles" :key="article.id">
          <Card :article="article" />
        </v-col>
      </v-row>
    </div>

    <br />
    <!-- <v-container fluid style="padding: 0px;" class="mb-4">
      <v-layout row align-center>
        <v-flex style="text-align: left;" class="mr-1" shrink>
          <v-card class="px-3">
            <h5 class="py-2 pl-2">
              Total Count:
              <span v-if="recordsTotalCount !== null">{{recordsTotalCount}}</span>

              <span v-else>
                <i style="font-weight: normal;">No Record Found</i>
              </span>
            </h5>
          </v-card>
        </v-flex>

        <v-flex />

        <v-flex xs1 shrink class="mr-1">
          <v-select
            class="requests-table-per-page-select"
            color="secondary"
            :items="patientsPerPageOptions"
            v-model="pagination.pageSize"
            auto
            @input="onChangePerPage"
            :loading="loadingTable"
            :disabled="loadingTable"
          ></v-select>
        </v-flex>

        <v-flex shrink>
          <div class="text-xs-center">
            <v-pagination
              :length="pagination.length"
              v-model="pagination.page"
              :total-visible="pagination.totalVisible"
              color="secondary"
              @input="onPaginationChange"
              :disabled="loadingTable"
            />
          </div>
        </v-flex>
      </v-layout>
    </v-container>-->
    <div class="text-center">
      <v-pagination
        :length="pagination.length"
        v-model="pagination.page"
        :total-visible="pagination.totalVisible"
        @input="onPaginationChange"
        :disabled="!isReady"
      />
      <!-- <v-pagination v-model="page" :length="6"></v-pagination> -->
    </div>
  </v-container>
</template>

<script>
import constants from "../constants";
import Card from "../components/Card.vue";

export default {
  name: "HelloWorld",
  inject: ["theme"],
  components: {
    Card
  },
  props: ["categoryId"],
  data: () => ({
    prefixURL: constants.API_PREFIX,
    isReady: false,
    loadingTable: false,
    articles: [],
    page: 1,
    pagination: {
      totalVisible: 7,
      length: 20,
      page: 1,
      pageSize: 5
    },
    patientsPerPageOptions: [5, 10, 20, 50, 100],
    recordsTotalCount: null
  }),
  computed: {
    grid() {
      console.log("this.$store.state.grid", typeof this.$store.state.grid);
      return this.$store.state.grid;
    }
  },
  async created() {
    scroll(0, 0);
  //  this.$getLocation({
  //     enableHighAccuracy: true, //defaults to false
  //     // timeout: Infinity, //defaults to Infinity
  //     maximumAge: 0 //defaults to 0

  //   })
  //     .then(coordinates => {
  //       console.log('coordinates', coordinates);
  //     });


    console.log("aaaaaaaaaaaaaaaaaaa", this.categoryId);
    this.search();
    // var result = await this.$http.getArticles({ categoryId: this.categoryId });
    // console.log(result);
    // this.articles = result.items;
    // this.recordsTotalCount = result.itemsCount;

    // this.pagination.length = result.pagesCount;
    // this.pagination.page = result.currentIndex;
  },
  methods: {
    async search() {
      this.$store.commit("loadData",true);

      var searchForm = { categoryId: this.categoryId };

      const result = await this.$http.getArticles({
        pageIndex: 1,
        pageSize: this.pagination.pageSize,
        ...searchForm
      });

      this.articles = result.items;

      this.recordsTotalCount = result.itemsCount;

      this.pagination.length = result.pagesCount;
      this.pagination.page = result.currentIndex;
      this.isReady = true;
      // this.orkrgList = this.orkrgList.filter(r =>  r.ordernr.includes(this.searchForm.ordernr))
      console.log("this.orkrgList", this.orkrgList);
      this.$store.commit("loadData",false);
    },
    async onChangePerPage(newVal) {
      this.isReady = false;
      this.$store.commit("loadData",true);

      var searchForm = { categoryId: this.categoryId };

      const result = await this.$http.getArticles({
        pageIndex: 1,
        pageSize: newVal,
        ...searchForm
      });

      this.articles = result.items;

      this.recordsTotalCount = result.itemsCount;

      this.pagination.length = result.pagesCount;
      this.pagination.page = result.currentIndex;

      this.isReady = true;
       this.$store.commit("loadData",false);
    },
    async onPaginationChange(index) {
      this.isReady = false;
       this.$store.commit("loadData",true);
      var searchForm = { categoryId: this.categoryId };

      const result = await this.$http.getArticles({
        pageIndex: index,
        pageSize: this.pagination.pageSize,
        ...searchForm
      });
      // this.statementList = [];

      this.articles = result.items;

      this.isReady = true;
       this.$store.commit("loadData",false);
      scroll(0, 0);
    }
  }
};
</script>
