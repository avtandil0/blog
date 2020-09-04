<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" :clipped="$vuetify.breakpoint.lgAndUp" app>
      <v-list dense>
        <template v-for="item in items">
          <v-row v-if="item.heading" :key="item.heading" align="center">
            <v-col cols="6">
              <v-subheader v-if="item.heading">{{ item.heading }}</v-subheader>
            </v-col>
            <v-col cols="6" class="text-center">
              <a href="#!" class="body-2 black--text">EDIT</a>
            </v-col>
          </v-row>
          <v-list-group
            v-else-if="item.children"
            :key="item.text"
            v-model="item.model"
            :prepend-icon="item.model ? item.icon : item['icon-alt']"
            append-icon
          >
            <template v-slot:activator>
              <v-list-item-content>
                <v-list-item-title>{{ item.text }}</v-list-item-title>
              </v-list-item-content>
            </template>
            <v-list-item
              v-for="(child, i) in item.children"
              :key="i"
              link
              @click="$router.push({ name: child.value, params: { categoryId: 2 } })"
            >
              <v-list-item-action v-if="child.icon">
                <v-icon
                  v-bind:style=" $route.path == '/'+item.value ? 'color:rgba(44, 130, 201, 1)' : '' "
                >{{ child.icon }}</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title
                  v-bind:style=" $route.path == '/'+item.value ? 'color:rgba(44, 130, 201, 1)' : '' "
                >{{ child.text }}</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
          <v-list-item
            v-else
            :key="item.text"
            link
            @click="$router.push({ name: item.value, params: { categoryId: 2 } })"
          >
            <v-list-item-action>
              <v-icon
                v-bind:style=" $route.path == '/'+item.value ? 'color:rgba(44, 130, 201, 1)' : '' "
              >{{ item.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>
                <span
                  v-bind:style=" $route.path == '/'+item.value ? 'color:rgba(44, 130, 201, 1)' : '' "
                >{{ item.text }}</span>
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app color="blue darken-3" dark>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
        <span class="hidden-sm-and-down">My Blog</span>
      </v-toolbar-title>
      <v-progress-linear
        :active="loading"
        :indeterminate="loading"
        absolute
        bottom
        color="deep-orange accent-4"
      ></v-progress-linear>
      <!-- <v-text-field
        flat
        solo-inverted
        hide-details
        prepend-inner-icon="mdi-magnify"
        label="Search"
        class="hidden-sm-and-down"
      ></v-text-field>-->
      <v-spacer></v-spacer>

      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn v-bind="attrs" icon v-on="on" v-if="currentUser">
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item v-for="(item, index) in addMenu" :key="index" @click="goToAddPage(item)">
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>

      <v-btn icon @click="changeCardsMode">
        <v-icon>mdi-apps</v-icon>
      </v-btn>
      <v-switch v-model="theme" hide-details inset label="შავი ფონი"></v-switch>

      <!-- <v-tooltip bottom v-if="currentUser">
        <template v-slot:activator="{ on }">
          <v-btn v-on="on" icon @click="$router.push({ name: 'QuillEditor' })">
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Add New</span>
      </v-tooltip>-->

      <v-tooltip bottom v-if="currentUser">
        <template v-slot:activator="{ on }">
          <v-btn v-on="on" icon @click="logOut()">
            <v-icon>mdi-logout</v-icon>
          </v-btn>
        </template>
        <span>Log Off</span>
      </v-tooltip>
    </v-app-bar>
    <v-content>
      <v-container>
        <router-view :key="$route.path" />
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
export default {
  name: "Home",
  props: {
    source: String,
  },
  data: () => ({
    currentUser: null,
    theme: false,
    dialog: false,
    drawer: null,
    rating: null,
    addMenu: [
      { title: "სტატია", path: "QuillEditor" },
      { title: "ვიდეო", path: "AddVideo" },
    ],
    items: [
      { icon: "mdi-home", text: "Home", value: "Main", categoryId: "111" },
      {
        icon: "mdi-cellphone-link",
        text: "App downloads",
        value: "Category1",
        categoryId: "2",
      },
      {
        icon: "mdi-cog",
        text: "Settings",
        value: "Category2",
        categoryId: "333",
      },

      {
        icon: "mdi-chevron-up",
        "icon-alt": "mdi-chevron-down",
        text: "More",
        model: false,
        children: [
          { icon: " mdi-check-all", text: "Import" },
          { icon: "mdi-airballoon", text: "Undo changes" },
          { icon: "mdi-video", text: "Other contacts" },
        ],
      },
      { icon: "mdi-contacts", text: "Contacts" },
      {
        icon: "mdi-play-circle",
        text: "Video",
        value: "Videos",
        categoryId: "444",
      },
      {
        icon: "mdi-help-circle",
        text: "Questions",
        value: "Questions",
        categoryId: "777",
      },
    ],
  }),
  methods: {
    goToAddPage(item) {
      console.log("item", item);
      this.$router.push(item.path);
    },
    changeCardsMode() {
      this.$store.commit("changeCardsMode");
    },
    async logOut() {
      console.log("log out");
      // var result = await this.$http.logout();
      // console.log("result", result);
      localStorage.clear();
      // window.location.replace(connstants.API_PREFIX + "#/Login");
      location.reload();

      // this.$router.push("/Login");
    },
  },
  mounted() {
    this.$vuetify.theme.isDark = localStorage.theme == "dark" ? true : false;
  },
  computed: {
    loading() {
      return this.$store.state.loading;
    },
  },
  watch: {
    theme() {
      this.$vuetify.theme.isDark = localStorage.theme == "dark" ? false : true;
      localStorage.theme = localStorage.theme == "dark" ? "light" : "dark";
    },
    // currentUser() {
    //   this.currentUser = localStorage.getItem("currentUser");
    // }
  },
  created() {
    console.log("localStorage.grid11111", localStorage.grid);
    this.currentUser = localStorage.getItem("currentUser");
    if (!localStorage.getItem("likedArray")) {
      localStorage.setItem("likedArray", JSON.stringify([]));
    }
  },
};
</script>